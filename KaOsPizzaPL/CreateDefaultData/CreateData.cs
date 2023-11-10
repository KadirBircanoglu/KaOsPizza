using KaOsPizzaBL.EmailSenderProcess;
using KaOsPizzaEL.IdentityModels;
using KaOsPizzaEL;
using Microsoft.AspNetCore.Identity;
using KaOsPizzaDL.ContextInfo;

namespace KaOsPizzaPL.CreateDefaultData
{
    public class CreateData
    {
        public void CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var emailManager = serviceProvider.GetRequiredService<IEmailManager>();

            //var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var context = serviceProvider.GetService<MyContext>();
            var configuration = serviceProvider.GetService<IConfiguration>();// appsettings.json dosyasına ulaşmak için

            string[] emails = configuration["ManagerEmails"].Split(',');
            var allRoles = Enum.GetNames(typeof(AllRoles));


            foreach (string role in allRoles)
            {
                var result = roleManager.RoleExistsAsync(role).Result; //rolden var mı?
                if (!result) //rolden yok!
                {
                    AppRole r = new AppRole()
                    {
                        InsertedDate = DateTime.Now,
                        Name = role,
                        IsDeleted = false,
                        Description = $"Sistem tarafından oluşturuldu"
                    };
                    var roleResult = roleManager.CreateAsync(r).Result;

                    //roleresulta bakalım
                    if (!roleResult.Succeeded)
                    {
                        //log email
                    }

                }

            }



        }
    }
}
