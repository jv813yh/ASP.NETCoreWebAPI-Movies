using System.Text.Json.Serialization;

namespace Movies.Api.DTOs
{
    public class MovieDTO
    {
        [JsonPropertyName("_id")]
        public uint MovieId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("isAvailable")]
        public bool IsAvailable { get; set; }

        [JsonPropertyName("dateAdded")]
        public DateTime DateAdded { get; set; }

        [JsonPropertyName("directorID")]
        public uint DirectorId { get; set; }

        [JsonPropertyName("actorsIDs")]
        public virtual List<uint> ActorsIds { get; set; } = new List<uint>();

        [JsonPropertyName("genres")]
        public virtual List<string> Genres { get; set; } = new List<string>();

    }
}
