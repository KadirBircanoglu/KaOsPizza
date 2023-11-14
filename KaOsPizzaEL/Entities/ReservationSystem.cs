using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.Entities
{
    public class ReservationSystem : BaseNumeric<long>
    {
        public DateTime Date {  get; set; }
        public bool AM0900 {  get; set; }
        public bool AM0930 { get; set; }
        public bool AM1000 { get; set; }
        public bool AM1030 { get; set; }
        public bool AM1100 { get; set; }
        public bool AM1130 { get; set; }
        public bool PM1200 { get; set; }
        public bool PM1230 { get; set; }
        public bool PM1300 { get; set; }
        public bool PM1330 { get; set; }
        public bool PM1400 { get; set; }
        public bool PM1430 { get; set; }
        public bool PM1500 { get; set; }
        public bool PM1530 { get; set; }
        public bool PM1600 { get; set; }
        public bool PM1630 { get; set; }
        public bool PM1700 { get; set; }
        public bool PM1730 { get; set; }
        public bool PM1800 { get; set; }
        public bool PM1830 { get; set; }
        public bool PM1900 { get; set; }
        public bool PM1930 { get; set; }
        public bool PM2000 { get; set; }
    }
}
