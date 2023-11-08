using AutoMapper;
using KaOsPizzaEL.Entities;
using KaOsPizzaEL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Food, FoodDTO>().ReverseMap();
            CreateMap<FoodType, FoodTypeDTO>().ReverseMap();
            CreateMap<Reservation, ReservationDTO>().ReverseMap();
            CreateMap<Services, ServicesDTO>().ReverseMap();
        }
    }
}
