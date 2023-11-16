using KaOsPizzaBL.InterfacesOfManagers;
using KaOsPizzaEL.Entities;
using KaOsPizzaEL.IdentityModels;
using KaOsPizzaEL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KaOsPizzaPL.Controllers
{
    public class AdminController : Controller
    {
        private readonly IFoodManager _foodManager;
        private readonly IFoodTypeManager _foodTypeManager;
        private readonly IReservationManager _reservationManager;
        private readonly IReservationSystemManager _reservationSystemManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(IFoodManager foodManager, IFoodTypeManager foodTypeManager, IReservationManager reservationManager, IReservationSystemManager reservationSystemManager, UserManager<AppUser> userManager)
        {
            _foodManager = foodManager;
            _foodTypeManager = foodTypeManager;
            _reservationManager = reservationManager;
            _reservationSystemManager = reservationSystemManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFood()
        {

            return View(new FoodDTO() { FoodTypes = _foodTypeManager.GetAll().Data.ToList() });
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


        public IActionResult Confirmation()
        {
            var rezervasyonTalepleri = _reservationManager.GetAllWithoutJoin(x => x.Confirmation == null && !x.IsDeleted).Data.ToList();
            var rezSys = _reservationSystemManager.GetAll(x => !x.IsDeleted).Data.ToList();

            foreach (var talep in rezervasyonTalepleri)
            {
                var customer = _userManager.FindByIdAsync(talep.UserId).Result;

                var eslesme = rezSys.FirstOrDefault(x => x.Id == talep.ReservationSystemId);

                talep.DateTime = eslesme.Date + eslesme.Time;

                talep.AppUser = customer;
            }


            return View(rezervasyonTalepleri);

        }



        [HttpPost]
        public IActionResult Confirmation(ReservationDTO model, int userId, bool confirmationStatus)
        {
            // Your logic here
            // Use the confirmationStatus variable to determine whether it was approved or rejected
            //userId

            var rezervasyonTalepleri = _reservationManager.GetAllWithoutJoin(x => x.Confirmation == null && !x.IsDeleted).Data.ToList();
            var rezervasyon = _reservationManager.GetbyId(userId).Data;

            model.Id = rezervasyon.Id;
            model.CreatedDate = rezervasyon.CreatedDate;
            model.IsDeleted = rezervasyon.IsDeleted;
            model.UserId = rezervasyon.UserId;
            model.DateTime = rezervasyon.DateTime;
            model.ReservationSystemId = rezervasyon.ReservationSystemId;
            model.NumberofPeople = rezervasyon.NumberofPeople;


            if (confirmationStatus == true)
            {
                model.Confirmation = true;
                _reservationManager.Update(model);
            }
            else if (confirmationStatus == false)
            {
                model.Confirmation = false;
                _reservationManager.Update(model);

            }
            return View(model);
        }





        public IActionResult ReservationSettings()
        {
            return View();
        }
    }
}
