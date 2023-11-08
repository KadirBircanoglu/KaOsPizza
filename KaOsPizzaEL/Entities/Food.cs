using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.Entities
{
    public class Food : BaseNumeric<long>
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string PhotoLink { get; set; }
        [StringLength(50)]
        public long FoodTypeId { get; set; }
        [StringLength(500)]
        public string FoodMetarials { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("FoodTypeId")]
        public virtual FoodType FoodType { get; set; }
    }
}
