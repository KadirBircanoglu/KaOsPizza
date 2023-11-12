using Microsoft.AspNetCore.Mvc;

namespace KaOsPizzaPL.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFood()
        {
            return View();
        }
    }
}
