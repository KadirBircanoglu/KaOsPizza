using Humanizer;
using KaOsPizzaBL.EmailSenderProcess;
using KaOsPizzaBL.InterfacesOfManagers;
using KaOsPizzaEL.Entities;
using KaOsPizzaEL.IdentityModels;
using KaOsPizzaEL.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IEmailManager _emailManager;

        public AdminController(IFoodManager foodManager, IFoodTypeManager foodTypeManager, IReservationManager reservationManager, IReservationSystemManager reservationSystemManager, UserManager<AppUser> userManager, IEmailManager emailManager)
        {
            _foodManager = foodManager;
            _foodTypeManager = foodTypeManager;
            _reservationManager = reservationManager;
            _reservationSystemManager = reservationSystemManager;
            _userManager = userManager;
            _emailManager = emailManager;
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
            var rezervasyonTalepleri = _reservationManager.GetAllWithoutJoin(x => !x.IsDeleted).Data.ToList();
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
        [Authorize]
        public IActionResult Confirmation(long rezerveID2, bool confirmationStatus)
        {

            //var rezervasyonTalepleri = _reservationManager.GetAllWithoutJoin(x => x.Confirmation == null && !x.IsDeleted).Data.ToList();
            var rezervasyon = _reservationManager.GetbyId(rezerveID2).Data;
            var customerId = rezervasyon.UserId;
            var customer1 = _userManager.FindByIdAsync(customerId).Result;


            rezervasyon.Confirmation = confirmationStatus;
            _reservationManager.Update(rezervasyon);



            string kabulMesaj;

            if (confirmationStatus)
            {
                kabulMesaj = $"<b>Merhaba {customer1.Name} {customer1.Surname},</b><br/>" +
          $"Rezervasyon talebiniz <i style=\"color:green;\">Kabul </i>edilmiştir! <br/>" +
          "<br/>Afiyet olsun 🍕";
            }
            else
            {
                kabulMesaj = $"<b>Merhaba {customer1.Name} {customer1.Surname},</b><br/>" +
                      $"Rezervasyon talebiniz <i style=\"color:red;\">Red </i>edilmiştir! <br/>" +
                      "<br/>Detaylı bilgilendirme için bizimle iletişime geçebilirsiniz." +
                      "<br/>0(2016)5454652";
            }

            _emailManager.SendEmailGmail(new EmailMessageModel()
            {
                Subject = "KaOs Pizza Rezervasyon Hk!",
                Body = $"{kabulMesaj}",
                To = customer1.Email, // Send email to each user with the 'ADMIN' role
            });


            var rezervasyonTalepleri = _reservationManager.GetAllWithoutJoin(x => !x.IsDeleted).Data.ToList();
            var rezSys = _reservationSystemManager.GetAll(x => !x.IsDeleted).Data.ToList();

            foreach (var talep in rezervasyonTalepleri)
            {
                var customer2 = _userManager.FindByIdAsync(talep.UserId).Result;

                var eslesme = rezSys.FirstOrDefault(x => x.Id == talep.ReservationSystemId);

                talep.DateTime = eslesme.Date + eslesme.Time;

                talep.AppUser = customer2;
            }

            return View(rezervasyonTalepleri);
        }

        public IActionResult ReservationSettings()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult ReservationSettings(ReservationSettingMV model)
        {
            try
            {
                bool erroryok = true;

                ReservationSystemDTO reservationSystemDTO = new ReservationSystemDTO();

                if (model.EndDate == null && model.EndClock == null)
                {
                    reservationSystemDTO.Date = model.StartDate;
                    reservationSystemDTO.Time = model.StartClock;
                    reservationSystemDTO.IsDeleted = false;
                    reservationSystemDTO.CreatedDate = DateTime.Now;

                    _reservationSystemManager.Add(reservationSystemDTO);
                }
                else if (model.EndDate == null && model.EndClock != null)
                {
                    for (var j = model.StartClock; j <= model.EndClock; j = j.Add(TimeSpan.FromMinutes(model.Interval)))
                    {
                        reservationSystemDTO.Date = model.StartDate;
                        reservationSystemDTO.Time = j;
                        reservationSystemDTO.IsDeleted = false;
                        reservationSystemDTO.CreatedDate = DateTime.Now;

                        erroryok = _reservationSystemManager.Add(reservationSystemDTO).IsSuccess;

                    }
                }
                else if (model.EndDate != null && model.EndClock == null)
                {
                    for (var i = model.StartDate; i <= model.EndDate; i = i.AddDays(1))
                    {
                        reservationSystemDTO.Date = i;
                        reservationSystemDTO.Time = model.StartClock;
                        reservationSystemDTO.IsDeleted = false;
                        reservationSystemDTO.CreatedDate = DateTime.Now;

                        erroryok = _reservationSystemManager.Add(reservationSystemDTO).IsSuccess;
                    }
                }
                else if (model.EndDate != null && model.EndClock != null)
                {
                    for (var i = model.StartDate; i <= model.EndDate; i = i.AddDays(1))
                    {
                        for (var j = model.StartClock; j <= model.EndClock; j = j.Add(TimeSpan.FromMinutes(model.Interval)))
                        {
                            reservationSystemDTO.Date = i;
                            reservationSystemDTO.Time = j;
                            reservationSystemDTO.IsDeleted = false;
                            reservationSystemDTO.CreatedDate = DateTime.Now;

                            erroryok = _reservationSystemManager.Add(reservationSystemDTO).IsSuccess;

                        }
                    }
                }

                if (!erroryok) { ViewBag.error = "Bir sorun oluştu"; };

                return View();
            }
            catch (Exception)
            {
                return View();
                throw;

            }

        }
    }
}
