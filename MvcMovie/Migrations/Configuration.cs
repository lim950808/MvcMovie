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
            //��� ���̱׷��̼� �Ŀ� �޼��带 ȣ�� Seed �մϴ�(��, ��Ű�� ������ �ֿܼ��� update-database ȣ��). �� �޼���� �̹� ���Ե� ���� ������Ʈ�ϰų� ���� ���� ��� �����մϴ�.
            //AddOrUpdate �޼��忡 ���޵� ù ��° �Ű� ������ ���� �̹� �ִ��� Ȯ���ϴ� �� ����� �Ӽ��� �����մϴ�. �����ϴ� �׽�Ʈ ������ �������� ��� ����� �� ������ Title �����ϱ� ������ �� �뵵�� �Ӽ��� ����� �� �ֽ��ϴ�.
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
