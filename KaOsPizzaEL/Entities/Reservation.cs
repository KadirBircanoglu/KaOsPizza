using KaOsPizzaEL.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.Entities
{
    public class Reservation : BaseNumeric<long>
    {
        public string UserId { get; set; } //FK
        public DateTime ReservationDate { get; set; }
        public int NumberofPeople { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

    }
}
