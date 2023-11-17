using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.ViewModels
{
    public class ReservationSettingMV
    {
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Başlangıç Saati")]
        public TimeSpan StartClock { get; set; }

        [Display(Name = "Bitiş Saati")]
        public TimeSpan? EndClock { get; set; }

        [Display(Name = "Dakika Aralığı")]
        public int Interval { get; set; }
    }
}
