namespace MvcMovie.Migrations
{
    using MvcMovie.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcMovie.Models.MovieDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcMovie.Models.MovieDBContext context)
        {
            //모든 마이그레이션 후에 메서드를 호출 Seed 합니다(즉, 패키지 관리자 콘솔에서 update-database 호출). 이 메서드는 이미 삽입된 행을 업데이트하거나 아직 없는 경우 삽입합니다.
            //AddOrUpdate 메서드에 전달된 첫 번째 매개 변수는 행이 이미 있는지 확인하는 데 사용할 속성을 지정합니다. 제공하는 테스트 동영상 데이터의 경우 목록의 각 제목이 Title 고유하기 때문에 이 용도로 속성을 사용할 수 있습니다.
            context.Movies.AddOrUpdate(i => i.Title,
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-1-11"),
                    Genre = "Romantic Comedy",
                    Rating = "PG",
                    Price = 7.99M
                },

                 new Movie
                 {
                     Title = "Ghostbusters ",
                     ReleaseDate = DateTime.Parse("1984-3-13"),
                     Genre = "Comedy",
                     Rating = "PG",
                     Price = 8.99M
                 },

                 new Movie
                 {
                     Title = "Ghostbusters 2",
                     ReleaseDate = DateTime.Parse("1986-2-23"),
                     Genre = "Comedy",
                     Rating = "PG",
                     Price = 9.99M
                 },

               new Movie
               {
                   Title = "Rio Bravo",
                   ReleaseDate = DateTime.Parse("1959-4-15"),
                   Genre = "Western",
                   Rating = "PG",
                   Price = 3.99M
               }
           );

        }
    }
}
