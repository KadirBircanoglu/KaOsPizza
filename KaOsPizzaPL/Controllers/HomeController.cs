using KaOsPizzaBL.InterfacesOfManagers;
using KaOsPizzaEL.IdentityModels;
using KaOsPizzaEL.ViewModels;
using KaOsPizzaPL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KaOsPizzaPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFoodManager _foodManager;
        private readonly IFoodTypeManager _foodTypeManager;
        private readonly IServicesManager _servicesManager;
        private readonly IReservationManager _reservationManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, IFoodManager foodManager, IFoodTypeManager foodTypeManager, IServicesManager servicesManager, IReservationManager reservationManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _logger = logger;
            _foodManager = foodManager;
            _foodTypeManager = foodTypeManager;
            _servicesManager = servicesManager;
            _reservationManager = reservationManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Services()
        {
            var allservices = _servicesManager.GetAll(x => !x.IsDeleted).Data;

            ViewBag.FootCount =_foodManager.GetAll(x=> !x.IsDeleted).Data.Count().ToString();

            ViewBag.CustomerCount = _userManager.GetUsersInRoleAsync("CUSTOMER").Result.Count;

            ViewBag.WorkerCount = _userManager.GetUsersInRoleAsync("ADMIN").Result.Count;

            return View(allservices);
        }

        public IActionResult Menu()
        {
            try
            {
                var allfoods = _foodManager.GetAll(x => !x.IsDeleted).Data;
                return View(allfoods);
            }
            catch (Exception ex)
            {
                // log
                return View(new List<FoodDTO>());
            }
        }
    }
}