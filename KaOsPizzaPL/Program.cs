using KaOsPizzaDL.ContextInfo;
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}