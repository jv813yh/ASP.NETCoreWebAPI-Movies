using Movies.Data.Models;
using System.Text.Json.Serialization;

namespace Movies.Api.DTOs
{
    // Data Transfer Object (DTO) for the Person entity 
    public class PersonDTO
    {
        [JsonPropertyName("_id")]
        public uint PersonId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; } = "";

        [JsonPropertyName("biography")]
        public string Biography { get; set; } = "";
        public PersonRole Role { get; set; }
    }
}
