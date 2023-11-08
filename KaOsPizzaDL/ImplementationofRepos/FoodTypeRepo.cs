using KaOsPizzaDL.ContextInfo;
using KaOsPizzaDL.InterfaceofRepos;
using KaOsPizzaEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaDL.ImplementationofRepos
{
    public class FoodTypeRepo : Repository<FoodType, long>, IFoodTypeRepo
    {
        public FoodTypeRepo(MyContext context) : base(context)
        {
        }
    }
}
