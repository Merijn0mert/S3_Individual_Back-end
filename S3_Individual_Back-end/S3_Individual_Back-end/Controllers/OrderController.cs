using Microsoft.AspNetCore.Mvc;

namespace S3_Individual_Back_end.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
