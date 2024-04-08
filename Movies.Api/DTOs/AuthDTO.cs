using System.ComponentModel.DataAnnotations;

namespace Movies.Api.DTOs
{
    // This class is used to transfer data between the API and the client
    // It is used to represent the authentication data
    public class AuthDTO
    {
        [EmailAddress]
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
