using AutoMapper;
using KaOsPizzaDL.InterfaceofRepos;
using KaOsPizzaEL.Entities;
using KaOsPizzaEL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaBL.ImplementationOfManagers
{
    public class ReservationManager : Manager<ReservationDTO,Reservation,long>
    {
        public ReservationManager(IReservationRepo repo, IMapper mapper) : base(repo, mapper, new string[] { "AppUser" })
        {
        }

    }
}
