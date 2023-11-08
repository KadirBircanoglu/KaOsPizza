using KaOsPizzaDL.ContextInfo;
using KaOsPizzaDL.InterfaceofRepos;
using KaOsPizzaEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaDL.ImplementationofRepos
{
    public class ReservationRepo : Repository<Reservation, long>, IReservationRepo
    {
        public ReservationRepo(MyContext context) : base(context)
        {
        }
    }
}
