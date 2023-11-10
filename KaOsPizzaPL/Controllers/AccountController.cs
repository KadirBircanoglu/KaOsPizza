using AutoMapper;
using KaOsPizzaBL.EmailSenderProcess;
using KaOsPizzaEL;
using KaOsPizzaEL.IdentityModels;
using KaOsPizzaPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace KaOsPizzaPL.Controllers
{
    public class AccountController : Controller
    {
        private IMapper _mapper;
        private ILogger<AccountController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailManager _emailManager;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AccountController(IMapper mapper, ILogger<AccountController> logger, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IEmailManager emailManager, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IPasswordHasher<AppUser> passwordHasher)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailManager = emailManager;
            _environment = environment;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string? email)
        {
            TempData["login"] = "login";
            return View("Login", email);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //Aynı mail adresinden sistemde var mı?
                var sameMailAddress = _userManager.FindByNameAsync(model.Email).Result;
                if (sameMailAddress != null)
                {
                    ModelState.AddModelError("", "Bu mail zaten sistemde kayıtlıdır!");
                    return View(model);
                }

                //Artık bu sisteme üye olabilir

                AppUser user = new AppUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    BirthDate = model.BirthDate,
                    PhoneNumber=model.PhoneNumber,
                    EmailConfirmed = false// Eğer zamanımız kalırsa bunu false yapıp aktivasyon işlemi ekleyeceğiz
                };

                var result = _userManager.CreateAsync(user, model.Password).Result;
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Yeni kişi kayıt edilemedi! Tekrar deneyiniz!");

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(model);
                }
                // user kayıt edildi.

                //Biz historical tablo oluşturduk ama bir de araştırdık ki identity paketinde
                //bu olay zaten düşünülmüş.... :) Bu durumda histori tablosunu kullanmayabilirsiniz
                //
                //UserForgotPasswordsHistoricalDTO u = new UserForgotPasswordsHistoricalDTO
                //{
                //    InsertedDate = DateTime.Now,
                //    IsDeleted = false,
                //    UserId = user.Id,
                //    Password = user.PasswordHash
                //};

                //_userForgotPasswordsHistoricalManager.Add(u);
                //Usera rol atamasi yapilacaktir
                var roleResult = _userManager.AddToRoleAsync(user, AllRoles.WAITINGFORACTIVATION.ToString()).Result;

                var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                var encToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                if (roleResult.Succeeded)
                {
                    var url = Url.Action("Activation", "Account", new { u = user.Id, t = encToken },
                        protocol: Request.Scheme);

                    _emailManager.SendEmailGmail(new EmailMessageModel()
                    {
                        Subject = "TechStore Aktivasyon İşlemi",
                        Body = $"<b>Merhaba {user.Name} {user.Surname},</b><br/>" +
                        $"Sisteme kaydınız başarılıdır! <br/>" +
                        $"Sisteme giriş yapmak için aktivasyonunuz gerçekleştirmek üzere <a href='{url}'>buraya</a> tıklayınız.",
                        To = user.Email
                    });

                    TempData["RegisterSuccessMsg"] = "Kayıt işlemi başarıdır!";

                    return RedirectToAction("Login", "Account", new { email = model.Email });

                }
                else
                {
                    TempData["RegisterFailMsg"] = "Kullanıcı kaydınız başarılıdır! Fakat rol ataması yapılamadığı için sistem yöneticisine başvurunuz!";
                    //log

                    return RedirectToAction("Login", "Account", new { email = model.Email });

                }



            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir sorun oldu!");
                //ex loglanacak
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Lütfen gerekli alanları dolduurnuz!");
                    return View("Login", email);
                }
                var user = _userManager.FindByEmailAsync(email).Result;
                if (user == null)
                {
                    ModelState.AddModelError("", "Lütfen sisteme üye olduğunuza emin olunuz!");
                    return View("Login", email);
                }


                if (_userManager.IsInRoleAsync(user, AllRoles.WAITINGFORACTIVATION.ToString()).Result)
                {
                    ModelState.AddModelError("", "DİKKAT! Sisteme giriş yapabilmeniz için emailinize gelen aktivasyon linkine tıklayınız! Aktivasyon işleminden sonra tekrar giriş yapmayı deneyiniz!");
                    return View("Login", email);
                }

                if (_userManager.IsInRoleAsync(user, AllRoles.PASSIVE.ToString()).Result)
                {
                    ModelState.AddModelError("", "DİKKAT! Sisteme giriş yapamazsınız. Çünkü kaydınızı sildirmişsiniz! Sistem yöneticisiyle görüşün!");
                    return View("Login", email);

                }
                if (_userManager.IsInRoleAsync(user, AllRoles.ADMIN.ToString()).Result)
                {
                    var signResult = _signInManager.PasswordSignInAsync(user, password, false, false).Result;

                    if (!signResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Lütfen eposta veya şifrenizi doğru yazsfdasgn");
                        return View("Login", email);
                    }

                    return RedirectToAction("Index", "Admin");
                }
                else if (_userManager.IsInRoleAsync(user, AllRoles.CUSTOMER.ToString()).Result)
                {
                    var signResult = _signInManager.PasswordSignInAsync(user, password, false, false).Result;

                    if (!signResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Lütfen eposta veya şifrenizi doğru yazsfdasgn");
                        return View("Login", email);
                    }

                    //Role göre sayfalara gidebilir.


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "DİKKAT! Sisteme giriş yapamazsınız. Çünkü rol atamanız yapılmamıştır. s. y.g.");
                    return View("Login", email);
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir sorun oldu!");
                //ex loglanacak
                return View("Login", email);
            }
        }


        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
