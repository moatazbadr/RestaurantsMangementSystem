using AutoMapper;
using Restaurant.Domain.Entities;

namespace Restaurants.Application.Dishs.Dtos
{
    public class DishesProfile : Profile
    {
        public DishesProfile() { 
        
            CreateMap<Dish,DishDto>();



        }
    }
}
