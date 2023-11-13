using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.Entities
{
    public class Services : BaseNumeric<long>
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public string FotoURL { get; set; }
    }
}
