using Microsoft.EntityFrameworkCore;
using Movies.Data.Models;

namespace Movies.Data.DbContexts
{
    public class MoviesDbContext : DbContext
    {
        DbSet<Person> People { get; set; }

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AddTestingData(modelBuilder);
        }

        private void AddTestingData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    PersonId = 1,
                    Name = "Tom Hanks",
                    BirthDate = new DateTime(1956, 7, 9),
                    Country = "USA",
                    Biography = "Thomas Jeffrey Hanks is an American actor and filmmaker. Known for both his comedic and dramatic roles, Hanks is one of the most popular and recognizable film stars worldwide.",
                    Role = PersonRole.Actor
                },
                new Person
                {
                    PersonId = 2,
                    Name = "Steven Spielberg",
                    BirthDate = new DateTime(1946, 12, 18),
                    Country = "USA",
                    Biography = "Steven Allan Spielberg is an American film director, producer, and screenwriter. He is considered one of the founding pioneers of the New Hollywood era and one of the most popular directors and producers in film history.",
                    Role = PersonRole.Director
                }
            );
        }
    }
}
