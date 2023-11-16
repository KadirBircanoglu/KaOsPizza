using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.Entities
{
    public class ReservationSystem : BaseNumeric<long>
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
