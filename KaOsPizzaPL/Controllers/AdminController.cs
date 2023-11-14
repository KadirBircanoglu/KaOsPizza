using KaOsPizzaBL.InterfacesOfManagers;
using KaOsPizzaEL.Entities;
using KaOsPizzaEL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KaOsPizzaPL.Controllers
{
    public class AdminController : Controller
    {
        private readonly IFoodManager _foodManager;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFood()
        {
            return View();
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
                var sameFood = _foodManager.GetAll(x => x.Id == model.Id).Data;
                if (sameFood != null && sameFood.Count > 0)
                {
                    ModelState.AddModelError("", "Bu ürün sistemde kayıtlıdır!");
                    return View(model);
                }

                // Ürün ekleme
                FoodDTO newFood = new FoodDTO()
                {
                    Name = model.Name,
                    Description = model.Description, // Nullabel??
                    Price = model.Price,
                    PhotoLink = model.PhotoLink,
                    FoodTypeId = model.FoodTypeId,
                    FoodMetarials = model.FoodMetarials,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };
                if (!_foodManager.Add(newFood).IsSuccess)
                {
                    ModelState.AddModelError("", "Yeni ürün kayıt edilemedi! Tekrar deneyiniz!");
                    return View(model);
                }


                return View(model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu! Tekrar deneyiniz!");
                return View(model);
            }
        }
    }
}
