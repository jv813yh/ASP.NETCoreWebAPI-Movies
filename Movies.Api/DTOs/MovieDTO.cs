using System.Text.Json.Serialization;

namespace Movies.Api.DTOs
{
    // This class is used to transfer data between the API and the client 
    // It is used to represent a movie 
    public class MovieDTO
    {
        [JsonPropertyName("_id")]
        public uint Id { get; set; }

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

        // This property is used to store the ids of the actors that are in the movie
        [JsonPropertyName("actorIDs")]
        public virtual List<uint> ActorsIds { get; set; } = new List<uint>();

        // This property is used to store the genres of the movie
        [JsonPropertyName("genres")]
        public virtual List<string> Genres { get; set; } = new List<string>();

    }
}
