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
    public class FoodTypeManager : Manager<FoodTypeDTO,FoodType,long>, IFoodTypeManager
    {
        public FoodTypeManager(IFoodTypeRepo repo, IMapper mapper) : base(repo, mapper, null)
        {
        }

    }
}
