using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.DTOs;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Movies.Api.Controllers
{
    // Controller for handling user authentication and authorization, 
    // user management and user storage
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Handles user management and their storage
        private readonly UserManager<IdentityUser> _userManager;

        // Handles user sign in and sign out
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Asynchronously registers a new user with the provided email and password
        /// </summary>
        /// <param name="authDto"></param>
        /// <returns></returns>
        [HttpPost("user")]
        public async Task<ActionResult> RegisterUser(AuthDTO authDto)
        {
            // Create a new user with the email and username from the authDto
            IdentityUser newUser = new IdentityUser
            {
                UserName = authDto.Email,
                Email = authDto.Email
            };

            // Create the new user in the database and return the result
            IdentityResult result = await _userManager.CreateAsync(newUser);

            if (result.Succeeded)
            {
                // Find the user by email 
                IdentityUser? user = await _userManager.FindByEmailAsync(authDto.Email);

                if (user != null)
                {
                    // Create a new user DTO
                    UserDTO userDto = ConvertToUserDto(user);

                    return Ok(userDto);
                }
            }
            
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Asynchronously logs in a user with the provided email and password
        /// </summary>
        /// <param name="authDto"></param>
        /// <returns></returns>
        [HttpPost("auth")]
        public async Task<ActionResult> LoginUser(AuthDTO authDto)
        {
            // Find the user by email 
            IdentityUser? user = await _userManager.FindByEmailAsync(authDto.Email);

            if (user == null)
            {
                return BadRequest("Invalid login attempt");
            }

            // Sign in the user with the email and password from the authDto
            // 3. parameter isPersistent: the user should stay logged in even after closing the browser
            // 4. parameter lockoutOnFailure: the user account should be locked after failed login
            SignInResult result = await _signInManager.PasswordSignInAsync(authDto.Email, authDto.Password, true, false);

            if (result.Succeeded)
            {
                // Create a new user DTO
                UserDTO userDto = ConvertToUserDto(user);

                return Ok(userDto);
            }

            return BadRequest("Invalid login attempt");
        }

        private UserDTO ConvertToUserDto(IdentityUser user)
        {
            bool isAdmin = false;

            return new UserDTO
            {
                UserId = user.Id,
                Email = user.Email,
                IsAdmin = isAdmin,
            };
        }
    }
}
