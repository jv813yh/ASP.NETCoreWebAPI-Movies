using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Data.Models
{
    // Enum for the role of the person
    public enum PersonRole
    {
        Actor,
        Director,
    }

    // Class for the person entity in the database with the following properties:
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public uint PersonId { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; } = "";

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Country { get; set; } = "";

        [Required]
        public string Biography { get; set; } = "";

        [Required]
        public PersonRole Role { get; set; }

        // Navigation property for the many-to-many relationship between Person and Movie
        public virtual List<Movie> MoviesAsDirector { get; set; } = new List<Movie>();

        // Navigation property for the many-to-many relationship between Person and Movie
        public virtual List<Movie> MoviesAsActor { get; set; } = new List<Movie>();
    }
}
