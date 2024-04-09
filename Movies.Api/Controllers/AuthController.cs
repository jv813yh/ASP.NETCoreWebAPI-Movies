using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.DTOs;


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
        public async Task<ActionResult> RegisterUserAsync(AuthDTO authDto)
        {
            // Create a new user with the email and username from the authDto
            IdentityUser newUser = new IdentityUser
            {
                UserName = authDto.Email,
                Email = authDto.Email
            };

            // Create the new user in the database and return the result
            IdentityResult result = await _userManager.CreateAsync(newUser, authDto.Password);

            if (result.Succeeded)
            {
                // Find the user by email 
                IdentityUser? user = await _userManager.FindByEmailAsync(authDto.Email);

                if (user != null)
                {
                    // Create a new user DTO
                    UserDTO userDto = await ConvertToUserDto(user);

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
        public async Task<ActionResult> LoginUserAsync(AuthDTO authDto)
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
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(authDto.Email, authDto.Password, true, false);

            if (result.Succeeded)
            {
                // Create a new user DTO
                UserDTO userDto = await ConvertToUserDto(user);

                return Ok(userDto);
            }

            return BadRequest("Invalid login attempt");
        }

        /// <summary>
        /// Asynchronously logs out the user
        /// </summary>
        /// <returns></returns>
        [HttpDelete("auth")]
        public async Task<ActionResult> LogOutUserAsync()
        {
            await _signInManager.SignOutAsync();

            // The client requires us that a successful response always contains one
            // value, so I pass an empty object to the Ok() method
            return Ok(new { });
        }

        /// <summary>
        /// Asynchronously gets the current user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("auth")]
        public async Task<ActionResult> GetCurrentUserAsync()
        {
            // Get the current user 
            IdentityUser? user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest("Invalid login attempt");
            }

            // Create a new user DTO
            UserDTO userDto = await ConvertToUserDto(user);

            return Ok(userDto);
        }

        private async Task<UserDTO> ConvertToUserDto(IdentityUser user)
        {
            bool isAdmin = await _userManager.IsInRoleAsync(user, UserRoles.Admin.ToString());

            return new UserDTO
            {
                UserId = user.Id,
                Email = user.Email,
                IsAdmin = isAdmin,
            };
        }
    }
}