using Movies.Data.Models;
using System.Text.Json.Serialization;

namespace Movies.Api.DTOs
{
    public class PersonDTO
    {
        public uint PersonId { get; set; }
        public string Name { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public string Country { get; set; } = "";
        public string Biography { get; set; } = "";
        public PersonRole Role { get; set; }
    }
}
