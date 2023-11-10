using AutoMapper.Extensions.ExpressionMapping;
using KaOsPizzaBL.EmailSenderProcess;
using KaOsPizzaBL.ImplementationOfManagers;
using KaOsPizzaBL.InterfacesOfManagers;
using KaOsPizzaDL.ContextInfo;
using KaOsPizzaDL.ImplementationofRepos;
using KaOsPizzaDL.InterfaceofRepos;
using KaOsPizzaEL.IdentityModels;
using KaOsPizzaEL.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KaOsPizzaPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //contexti ayarliyoruz.
            builder.Services.AddDbContext<MyContext>(options =>
            {
                //klasik mvcde connection string web configte yer alir.
                //core mvcde connection string appsetting.json dosyasindan alinir.
                options.UseSqlServer(builder.Configuration.GetConnectionString("KaOsPizzaCon"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });


            //appuser ve approle identity ayari
            builder.Services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireDigit = true;
                opt.User.RequireUniqueEmail = true;
                //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+&%";

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<MyContext>();



            //automapper ayari 
            builder.Services.AddAutoMapper(a =>
            {
                a.AddExpressionMapping();
                //a.AddProfile(typeof(Maps));
                //a.CreateMap<AppUser, ProfileViewModel>();

            });


            //interfacelerin DI yasam dongusu

            builder.Services.AddScoped<IEmailManager, EmailManager>();

            builder.Services.AddScoped<IFoodRepo, FoodRepo>();
            builder.Services.AddScoped<IFoodManager, FoodManager>();

            builder.Services.AddScoped<IFoodTypeRepo, FoodTypeRepo>();
            builder.Services.AddScoped<IFoodTypeManager, FoodTypeManager>();

            builder.Services.AddScoped<IReservationRepo, ReservationRepo>();
            builder.Services.AddScoped<IReservationManager, ReservationManager>();

            builder.Services.AddScoped<IServicesRepo, ServicesRepo>();
            builder.Services.AddScoped<IServicesManager, ServicesManager>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication, Authorization ayarý
            app.UseAuthentication(); // login logout
            app.UseAuthorization();  // yetki

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}