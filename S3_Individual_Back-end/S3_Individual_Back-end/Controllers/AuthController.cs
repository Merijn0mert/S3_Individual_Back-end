using BusinessLogic.Classes;
using BusinessLogic.Containers;
using DataAccess.DAL;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json;

namespace S3_Individual_Back_end.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        UserContainer container = new UserContainer(new UserDAL());
        
        [HttpPost]
        public async Task<IActionResult> login([FromBody] JsonElement data) 
        {
            var login = JsonSerializer.Deserialize<string>(data);
            var login2 = login;
            User user = new User();
            //string user = JsonSerializer.Deserialize<string>(login);
            if (ModelState.IsValid) // modelstate is true with null
            {

            }
            return Ok();
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
    }
}
