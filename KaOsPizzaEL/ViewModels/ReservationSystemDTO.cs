using KaOsPizzaEL.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.ViewModels
{
    public class ReservationSystemDTO
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

    }
}
