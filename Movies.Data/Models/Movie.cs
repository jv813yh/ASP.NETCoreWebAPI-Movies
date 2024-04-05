using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Data.Models
{
    // Movie entity class that represents the Movie table in the database
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public uint Id { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        // Foreign key for the Director relationship
        [ForeignKey(nameof(Director))]
        public uint DirectorId { get; set; }

        // Navigation property for the Director relationship
        public virtual Person? Director { get; set; }

        // Navigation property for the many-to-many relationship between Movie and Genre
        public virtual List<Genre> Genres { get; set; } = new List<Genre>();

        // Navigation property for the many-to-many relationship between Movie and Person
        public virtual List<Person> Actors { get; set; } = new List<Person>();

    }
}
