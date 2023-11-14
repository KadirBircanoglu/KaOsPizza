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

    public class ReservationSystemRepo : Repository<ReservationSystem, long>, IReservationSystemRepo
    {
        public ReservationSystemRepo(MyContext context) : base(context)
        {
        }
    }
}
