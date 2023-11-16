using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.ViewModels
{
    public class RezervasyonViewModel
    {
        public List<long> ReserveIdList { get; set; }
        public List<ReservationSystemDTO> ReservationSystemList { get; set; }
    }
}
