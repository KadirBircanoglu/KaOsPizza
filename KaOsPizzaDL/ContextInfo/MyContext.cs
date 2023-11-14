using KaOsPizzaEL.Entities;
using KaOsPizzaEL.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaDL.ContextInfo
{
    public class MyContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public MyContext(DbContextOptions<MyContext> opt)
            : base(opt)
        {

        }

        public virtual DbSet<Food> FoodTable { get; set; }
        public virtual DbSet<FoodType> FoodTypeTable { get; set; }
        public virtual DbSet<Reservation> ReservationTable { get; set; }
        public virtual DbSet<ReservationSystem> ReservationSystemTable { get; set; }
        public virtual DbSet<Services> ServicesTable { get; set; }


    }
}
