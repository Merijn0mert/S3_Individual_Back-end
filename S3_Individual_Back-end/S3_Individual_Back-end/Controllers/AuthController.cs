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
        public async Task<IActionResult> SubmitLogin([FromForm] Login login) 
        {
            //string user = JsonSerializer.Deserialize<string>(login);
            if (ModelState.IsValid) // modelstate is true with null
            {
                try
                {
                    User validatedUser = container.AttemptLogin(login.Email, login.Password);

                    if (validatedUser.Email != null)
                    {
                        HttpContext.Session.SetInt32("UserID", validatedUser.UserID);
                        HttpContext.Session.SetString("UserName", validatedUser.Name);
                        HttpContext.Session.SetString("UserEmail", validatedUser.Email);
                        HttpContext.Session.SetInt32("RolID", validatedUser.Rolid);

                        return RedirectToAction("Index", "Home");
                    }
                    return Ok(login);
                }

                catch (Exception ex)
                {                   
                    return Ok(ex);
                }
            }
            return Ok();
        }
    }
}
