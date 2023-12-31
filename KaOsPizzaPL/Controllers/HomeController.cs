﻿using AutoMapper.Extensions.ExpressionMapping;
using KaOsPizzaBL.EmailSenderProcess;
using KaOsPizzaBL.InterfacesOfManagers;
using KaOsPizzaDL.ContextInfo;
using KaOsPizzaEL.Entities;
using KaOsPizzaEL.IdentityModels;
using KaOsPizzaEL.ViewModels;
using KaOsPizzaPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;
using System.Security.Policy;

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
        private readonly IReservationSystemManager _reservationSystemManager;
        private readonly IEmailManager _emailManager;

        public HomeController(ILogger<HomeController> logger, IFoodManager foodManager, IFoodTypeManager foodTypeManager, IServicesManager servicesManager, IReservationManager reservationManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, MyContext myContext, IReservationSystemManager reservationSystemManager, IEmailManager emailManager)
        {
            _logger = logger;
            _foodManager = foodManager;
            _foodTypeManager = foodTypeManager;
            _servicesManager = servicesManager;
            _reservationManager = reservationManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _myContext = myContext;
            _reservationSystemManager = reservationSystemManager;
            _emailManager = emailManager;
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
        [HttpPost]
        public IActionResult Contact(ContactVM model)
        {
            if(model != null)
            {
                var usersWithAdminRole = _userManager.GetUsersInRoleAsync("ADMIN").Result;

                var adminEmails = usersWithAdminRole.Select(user => user.Email);

                // Send email to all users with the 'ADMIN' role
                foreach (var email in adminEmails)
                {
                    _emailManager.SendEmailGmail(new EmailMessageModel()
                    {
                        Subject = "Birisi Kaos pizza ile iletişim kurmak istiyor!!",
                        Body = $"<b>Merhaba Yetkili,</b><br/>" +
                               $"Az önce {model.NameSurname} isimli kişi Kaos pizza ile iletişim kurmak istediğini belirtti. İçeriği aşağıdadır.<br/><br/><br/>" +
                               $"Konu: {model.Subject} <br/> Ad & Soyad: {model.NameSurname} <br/> Numarası: {model.Number} <br/> E-Postası: {model.Email} <br/><br/>" +
                               $"Mesajı: {model.Message}",
                        To = email, // Send email to each user with the 'ADMIN' role
                    });
                }
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Reservation()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Reservation(string rezervetarih, string secilensaat,int kisiSayisi)
        {
            if (secilensaat == null)
            {
                TempData["rezrvtarihv"] = rezervetarih;
                var rzvrtrh = DateTime.ParseExact(rezervetarih, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                RezervasyonViewModel rezervasyonViewModel = new RezervasyonViewModel()
                {
                    ReservationSystemList = _reservationSystemManager.GetAll(x => x.Date == rzvrtrh && !x.IsDeleted).Data.ToList()
                };


                List<long> rzvrsytemidlist = new List<long>();

                foreach (var item in rezervasyonViewModel.ReservationSystemList)
                {
                    rzvrsytemidlist.Add(item.Id);
                }

                var reservasnContr = _reservationManager.GetAll(x => rzvrsytemidlist.Contains(x.ReservationSystemId) && !x.IsDeleted).Data.ToList();

                rzvrsytemidlist.Clear();

                foreach (var item in reservasnContr)
                {
                    rzvrsytemidlist.Add(item.ReservationSystemId);
                }

                rezervasyonViewModel.ReserveIdList = rzvrsytemidlist;

                return View(rezervasyonViewModel);
            }
            else 
            {
                var rzvrtrh = DateTime.ParseExact(rezervetarih, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                TimeSpan timeee = TimeSpan.ParseExact(secilensaat, "hh\\:mm", CultureInfo.InvariantCulture);

                var reserveolacak = _reservationSystemManager.GetByConditionWithoutJoin(x => x.Date == rzvrtrh && x.Time == timeee && !x.IsDeleted).Data;



                ReservationDTO reservationDTO = new ReservationDTO()
                {
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    ReservationSystemId = reserveolacak.Id,
                    NumberofPeople=kisiSayisi,

                };

                var result = _reservationManager.Add(reservationDTO);

                if(result.IsSuccess)
                {
                    ViewBag.Kaydedildi = "Başarılı bir şekilde rezervasyon talep ettiniz. Rezervasyonunuz onaylandığında E-postanıza mail gönderilecektir.";
                }
                return View(); 
            }
        }

        public IActionResult Services()
        {
            var allservices = _servicesManager.GetAll(x => !x.IsDeleted).Data;

            ViewBag.FootCount =_foodManager.GetAll(x=> !x.IsDeleted).Data.Count().ToString();

            ViewBag.CustomerCount = _userManager.GetUsersInRoleAsync("CUSTOMER").Result.Count;

            List<string> workers = new List<string>();
            workers.Add("ADMIN");
            workers.Add("CHEF");
            workers.Add("WAITER");

            int x = 0;
            foreach (var workerRole in workers)
            {
                 x = x + (_userManager.GetUsersInRoleAsync(workerRole).Result.Count);
            }


            ViewBag.WorkerCount = x;
            

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