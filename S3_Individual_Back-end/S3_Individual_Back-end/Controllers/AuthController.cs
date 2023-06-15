using BusinessLogic.Classes;
using BusinessLogic.Containers;
using DataAccess.DAL;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Interface.DTO;

namespace S3_Individual_Back_end.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        UserContainer container = new UserContainer(new UserDAL());

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] JsonElement data) 
        {
            Login _user = new Login
            {
                Email = data.GetProperty("email").GetString(),
                Password = data.GetProperty("password").GetString()
            };

                try
                {
                    User validatedUser = container.AttemptLogin(_user.Email, _user.Password);
                    if (validatedUser.Email != null)
                    {
                    var token = GenerateToken(validatedUser);
                    // Successful login
                    return Ok(token);
                    }
                    else
                    {
                        // Unauthorized login
                        return Unauthorized();
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occurred during authentication
                    // Log the exception or perform any other necessary actions

                    // Return a generic server error response
                    return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred during login.");
                }

        }
       
        [HttpPost("signup")]
        public async Task<IActionResult> SubmitRegister([FromForm] User user) 
        {
            if (ModelState.IsValid)
            {
                container.CreateUser(user);

                return Ok(user);
            }
            return Ok(user);
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(          
                claims: claims,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
