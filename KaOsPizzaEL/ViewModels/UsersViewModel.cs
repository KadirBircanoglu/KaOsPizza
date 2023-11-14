using KaOsPizzaEL.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.ViewModels
{
    public class UsersViewModel
    {
        public List<AppUser> Customers { get; set; }
        public List<AppUser> Chefs { get; set; }
        public List<AppUser> Waiters { get; set; }

    }
}
