using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.ViewModels
{
    public class MenuPartViewModel
    {
        public List<FoodDTO> Pizzas { get; set; }
        public List<FoodDTO> Pastas { get; set; }
        public List<FoodDTO> Burgers { get; set; }
        public List<FoodDTO> Drinks { get; set; }
        public List<FoodDTO> AllFoods { get; set; }

    }
}
