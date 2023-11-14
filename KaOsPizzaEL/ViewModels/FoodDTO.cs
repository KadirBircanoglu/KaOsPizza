using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.ViewModels
{
    public class FoodDTO
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }                                                                         
        public decimal Price { get; set; }
        public string PhotoLink { get; set; }
        public long FoodTypeId { get; set; }
        public string FoodMetarials { get; set; }

        public List<FoodTypeDTO?>? FoodTypes { get; set; }
    }
}
