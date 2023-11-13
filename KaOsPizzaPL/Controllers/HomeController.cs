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

        public IActionResult Menu()
        {
            try
            {
                MenuPartViewModel menuPartViewModel = new MenuPartViewModel();
                menuPartViewModel.Pizzas = _foodManager.GetAll(x => !x.IsDeleted && x.FoodTypeId == 1).Data.ToList();
                menuPartViewModel.Pastas = _foodManager.GetAll(x => !x.IsDeleted && x.FoodTypeId == 2).Data.ToList();
                menuPartViewModel.Burgers = _foodManager.GetAll(x => !x.IsDeleted && x.FoodTypeId == 3).Data.ToList();
                menuPartViewModel.Drinks = _foodManager.GetAll(x => !x.IsDeleted && x.FoodTypeId == 4).Data.ToList();
                menuPartViewModel.AllFoods = _foodManager.GetAll(x => !x.IsDeleted).Data.ToList();


                return View(menuPartViewModel);
            }
            catch (Exception ex)
            {
                // log
                return View(new List<FoodDTO>());
            }
        }
    }
}