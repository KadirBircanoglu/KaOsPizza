﻿using AutoMapper;
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
    public class ServicesManager : Manager<ServicesDTO,Services,long>, IServicesManager
    {
        public ServicesManager(IServicesRepo repo, IMapper mapper) : base(repo, mapper, null)
        {
        }

    }
}
