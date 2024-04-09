using System.Text.Json.Serialization;

namespace Movies.Api.DTOs
{
    // Class representing a user in the app
    public class UserDTO
    {
        [JsonPropertyName("_id")]
        public string UserId { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsAdmin { get; set; }
    }
}
