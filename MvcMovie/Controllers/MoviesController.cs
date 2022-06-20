using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private MovieDBContext db = new MovieDBContext();

        // GET: Movies
        //Movies 테이블의 모든 항목을 Movies 반환한 다음 결과를 뷰에 Index 전달합니다. 클래스의 MoviesController 다음 줄은 앞에서 설명한 대로 동영상 데이터베이스 컨텍스트를 인스턴스화합니다. 동영상 데이터베이스 컨텍스트를 사용하여 영화를 쿼리, 편집 및 삭제할 수 있습니다.
        public ActionResult Index(string movieGenre, string searchString)
        {
            var GenreLst = new List<string>();

            // 데이터베이스에서 모든 장르를 검색하는 LINQ 쿼리
            var GenreQry = from d in db.Movies
                           orderby d.Genre
                           select d.Genre;
            //제네릭 List 컬렉션의 메서드를 사용하여 AddRange 목록에 모든 고유 장르를 추가합니다.
            GenreLst.AddRange(GenreQry.Distinct());
            //SelectList 개체 ViewBag로 저장한 다음 드롭다운 목록 상자에서 범주 데이터에 액세스하는 것이 MVC 애플리케이션의 일반적인 방법입니다.
            ViewBag.movieGenre = new SelectList(GenreLst);

            //LINQ 쿼리를 만들어 영화를 선택합니다.
            var movies = from m in db.Movies
                         select m;
            //매개 변수에 searchString 문자열이 포함된 경우 영화 쿼리는 다음 코드를 사용하여 검색 문자열의 값을 필터링하도록 수정됩니다.
            if (!String.IsNullOrEmpty(searchString))
            {
                //s => s.Title 코드는 람다 식입니다. 람다는 위의 코드에서 사용되는 Where 메서드와 같은 표준 쿼리 연산자 메서드에 대한 인수로 메서드 기반 LINQ 쿼리에서 사용됩니다. LINQ 쿼리는 정의되거나 메서드를 호출 WhereOrderBy하여 수정될 때 실행되지 않습니다. 대신 쿼리 실행이 지연됩니다. 즉, 실현된 값이 실제로 반복되거나 ToList 메서드가 호출될 때까지 식의 평가가 지연됩니다.
                //Contains 메서드는 위의 c# 코드가 아닌 데이터베이스에서 실행됩니다. 데이터베이스에서 대/소문자를 구분하지 않는 SQL LIKE에 대한 맵을 포함합니다.
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            //매개 변수를 확인하는 movieGenre 방법을 보여줍니다. 비어 있지 않은 경우 코드는 영화 쿼리를 추가로 제한하여 선택한 영화를 지정된 장르로 제한합니다.
            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            return View(movies);
        }

        // GET: Movies/Details/5
        //매개 변수는 id 일반적으로 경로 데이터로 전달됩니다. 예를 들어 http://localhost:1234/movies/details/1 컨트롤러를 영화 컨트롤러, 작업 및 id 1로 details 설정합니다. 다음과 같이 쿼리 문자열을 사용하여 ID를 전달할 수도 있습니다.
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            //Movie 찾은 경우 모델의 인스턴스가 Movie 뷰에 Details 전달됩니다.
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // ValidateAntiForgeryToken 특성은 요청 위조를 방지하는 데 사용되며 편집 보기 파일(Views\Movies\Edit.cshtml)에서 쌍을 @Html.AntiForgeryToken() 이룹니다.
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
