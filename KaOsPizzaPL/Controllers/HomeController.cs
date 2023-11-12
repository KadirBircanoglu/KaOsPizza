using KaOsPizzaBL.InterfacesOfManagers;
using KaOsPizzaEL.ViewModels;
using KaOsPizzaPL.Models;
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

        public HomeController(ILogger<HomeController> logger, IFoodManager foodManager, IFoodTypeManager foodTypeManager, IServicesManager servicesManager, IReservationManager reservationManager)
        {
            _logger = logger;
            _foodManager = foodManager;
            _foodTypeManager = foodTypeManager;
            _servicesManager = servicesManager;
            _reservationManager = reservationManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
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