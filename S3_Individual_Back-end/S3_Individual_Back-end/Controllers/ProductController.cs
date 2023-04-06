using Microsoft.AspNetCore.Mvc;

namespace S3_Individual_Back_end.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
