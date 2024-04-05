using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Data.Models
{
    // Genre entity with data annotations for the database schema and relationships
    [Index(nameof(Name), IsUnique = true)]
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public uint GenreId { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; } = string.Empty;


        // Navigation property for the many-to-many relationship between Genre and Movie
        public virtual List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
