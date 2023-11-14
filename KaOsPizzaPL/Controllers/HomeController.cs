using AutoMapper.Extensions.ExpressionMapping;
using KaOsPizzaBL.InterfacesOfManagers;
using KaOsPizzaDL.ContextInfo;
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
        private readonly MyContext _myContext;

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
            var coverphoto1 = _foodManager.GetByCondition(x=>x.Name== "Italyan Pizza").Data;
            ViewBag.coverphoto1_Name = coverphoto1.Name;
            ViewBag.coverphoto1_Description = coverphoto1.Description;

            var coverphoto2 = _foodManager.GetByCondition(x => x.Name == "Margherita").Data;
            ViewBag.coverphoto2_Name = coverphoto2.Name;
            ViewBag.coverphoto2_Description = coverphoto2.Description;

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

        public IActionResult Crew()
        {
            try
            {
                UsersViewModel usersViewModel = new UsersViewModel();
                //usersViewModel.Customers = _userManager.GetUsersInRoleAsync("CUSTOMER").Result.ToList();
                usersViewModel.Chefs = _userManager.GetUsersInRoleAsync("CHEF").Result.ToList();
                usersViewModel.Waiters = _userManager.GetUsersInRoleAsync("WAITER").Result.ToList();

                return View(usersViewModel);
            }
            catch (Exception ex)
            {
                // log
                return View(new List<AppUser>());
            }
        }
    }
}