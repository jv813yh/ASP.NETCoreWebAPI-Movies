using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Movies.Data.Models;

namespace Movies.Data.DbContexts
{
    public class MoviesDbContext : DbContext
    {
        // DbSet for the Person entity
        DbSet<Person> People { get; set; }

        // DbSet for the Movie entity
        DbSet<Movie> Movies { get; set; }

        // DbSet for the Genre entity
        DbSet<Genre> Genres { get; set; }

        public MoviesDbContext(DbContextOptions<MoviesDbContext> options): base(options)
        {
            
        }

        // Method is part of a class derived from DbContext EF.
        // Allows to define configurations, mapping, setting keys ...
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define the relationship between the entities (one-to-many) 
            modelBuilder
                .Entity<Movie>()
                .HasOne(m => m.Director)
                .WithMany(p => p.MoviesAsDirector);

            // Define the relationship between the entities (many-to-many)
            modelBuilder
                .Entity<Movie>()
                .HasMany(m => m.Actors)
                .WithMany(p => p.MoviesAsActor)
                .UsingEntity(j => j.ToTable("MovieActors"));

            // Ca
            IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
              .SelectMany(type => type.GetForeignKeys())
              .Where(foreignKey => !foreignKey.IsOwnership && foreignKey.DeleteBehavior == DeleteBehavior.Cascade);

            // Change the delete behavior of the foreign keys to restrict
            foreach (IMutableForeignKey foreignKey in cascadeFKs)
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;


            // 
            AddTestingData(modelBuilder);
        }

        // Method to add testing data to the database
        private void AddTestingData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person
                {
                    PersonId = 1,
                    Name = "Brad Pitt",
                    BirthDate = new DateTime(1963, 12, 18),
                    Country = "USA",
                    Biography = "William Bradley \"Brad\" Pitt is an American film actor and producer, winner of two Golden Globe Awards for Best" +
                    " Supporting Actor for his performance in Twelve Monkeys and Once Upon a Time in Hollywood.",
                    Role = PersonRole.Actor
                },
                new Person
                {
                    PersonId = 2,
                    Name = "Quentin Tarantino",
                    BirthDate = new DateTime(1963, 03, 27),
                    Country = "USA",
                    Biography = "Quentin Jerome Tarantino is an American film director, screenwriter and actor who became famous in the " +
                    "early 1990s as a fresh, harsh and darkly humorous storyteller who brought new life to traditional American archetypes.",
                    Role = PersonRole.Director
                },
                new Person
                {
                    PersonId = 3,
                    Name = "Leonardo DiCaprio",
                    BirthDate = new DateTime(1974, 11, 11),
                    Country = "USA",
                    Biography = "Leonardo Wilhelm DiCaprio is an American actor, film producer, and environmentalist. He has often played " +
                    "unconventional roles, particularly in biopics and period films.",
                    Role = PersonRole.Actor
                },
                new Person
                {
                    PersonId = 4,
                    Name = "Christopher Nolan",
                    BirthDate = new DateTime(1970, 7, 30),
                    Country = "UK",
                    Biography = "Christopher Edward Nolan is a British-American film director, producer, and screenwriter. His films have " +
                    "grossed over $5 billion worldwide, and he is one of the highest-grossing directors in history.",
                    Role = PersonRole.Director
                },
                new Person
                {
                    PersonId = 5,
                    Name = "Christian Bale",
                    BirthDate = new DateTime(1974, 1, 30),
                    Country = "UK",
                    Biography = "Christian Charles Philip Bale is an English actor. Known for his versatility and intensive method acting, " +
                    "he is the recipient of many awards, including an Academy Award and two Golden Globe Awards.",
                    Role = PersonRole.Actor
                }
            );

            // Add genres to the database
            modelBuilder.Entity<Genre>().HasData(
            new Genre { GenreId = 1, Name = "sci-fi" },
            new Genre { GenreId = 2, Name = "adventure" },
            new Genre { GenreId = 3, Name = "action" },
            new Genre { GenreId = 4, Name = "romantic" },
            new Genre { GenreId = 5, Name = "animated" },
            new Genre { GenreId = 6, Name = "comedy" });
        }
    }
}
