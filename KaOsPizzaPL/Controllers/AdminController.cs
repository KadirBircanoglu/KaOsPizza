using KaOsPizzaBL.InterfacesOfManagers;
using KaOsPizzaEL.Entities;
using KaOsPizzaEL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KaOsPizzaPL.Controllers
{
    public class AdminController : Controller
    {
        private readonly IFoodManager _foodManager;
        private readonly IFoodTypeManager _foodTypeManager;

        public AdminController(IFoodManager foodManager, IFoodTypeManager foodTypeManager)
        {
            _foodManager = foodManager;
            _foodTypeManager = foodTypeManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFood()
        {
            
            return View(new FoodDTO() { FoodTypes= _foodTypeManager.GetAll().Data.ToList() });
        }

        [HttpPost]
        public IActionResult AddFood(FoodDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                // Bu ürün menüde mevcut mu?
                var sameFood = _foodManager.GetAll(x => x.Name == model.Name).Data;
                if (sameFood != null && sameFood.Count > 0)
                {
                    ModelState.AddModelError("", "Bu ürün sistemde kayıtlıdır!");
                    return View(model);
                }

                // Ürün ekleme
                FoodDTO newFood = new FoodDTO()
                {
                    Name = model.Name,
                    Description = model.Description, 
                    Price = model.Price,
                    PhotoLink = model.PhotoLink,
                    FoodTypeId = model.FoodTypeId,
                    FoodMetarials = model.FoodMetarials,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    FoodTypes = _foodTypeManager.GetAll().Data.ToList()
                };
                if (!_foodManager.Add(newFood).IsSuccess)
                {
                    ModelState.AddModelError("", "Yeni ürün kayıt edilemedi! Tekrar deneyiniz!");
                    return View(model);
                }

                ViewBag.Success = "true";

                return View(model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu! Tekrar deneyiniz!");
                return View(model);
            }
        }

        public  IActionResult Confirmation()
        {
            return View();
        }
    }
}
