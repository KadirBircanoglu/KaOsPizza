using AutoMapper;
using KaOsPizzaBL.InterfacesOfManagers;
using KaOsPizzaDL.InterfaceofRepos;
using KaOsPizzaEL.Entities;
using KaOsPizzaEL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaBL.ImplementationOfManagers
{
    public class FoodManager : Manager<FoodDTO, Food,long>, IFoodManager
    {
        public FoodManager(IFoodRepo repo, IMapper mapper) : base(repo, mapper, new string[] { "FoodType" })
        {

        }
    }
}
