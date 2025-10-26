using AutoMapper;
using Restaurant.Domain.Entities;
using Restaurants.Application.Dishs.Commands.CreateDish;

namespace Restaurants.Application.Dishs.Dtos
{
    public class DishesProfile : Profile
    {
        public DishesProfile() { 
        
            CreateMap<Dish,DishDto>();
            CreateMap<CreateDishCommand, Dish>();


        }
    }
}
