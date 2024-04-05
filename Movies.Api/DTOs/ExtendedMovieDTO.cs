using System.Text.Json.Serialization;

namespace Movies.Api.DTOs
{
    // 
    public class ExtendedMovieDTO : MovieDTO
    {
        [JsonPropertyName("director")]
        public PersonDTO Director { get; set; } = new PersonDTO();

        [JsonPropertyName("actors")]
        public List<PersonDTO> Actors { get; set; } = new List<PersonDTO>();
    }
}
