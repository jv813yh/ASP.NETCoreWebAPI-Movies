using Movies.Data.Models;
using System.Text.Json.Serialization;

namespace Movies.Api.DTOs
{
    // Data Transfer Object (DTO) for the Person entity 
    public class PersonDTO
    {
        [JsonPropertyName("_id")]
        public uint PersonId { get; set; }
        public string Name { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public string Country { get; set; } = "";
        public string Biography { get; set; } = "";
        public PersonRole Role { get; set; }
    }
}
