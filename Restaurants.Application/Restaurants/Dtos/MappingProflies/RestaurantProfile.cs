using AutoMapper;
using Restaurant.Domain.Entities;
using Restaurants.Application.Restaurants.Commands.RestaurantCommand;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restaurants.Application.Restaurants.Dtos.MappingProflies
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            // source -> target
            // هنا يعني انو لما تجي من الدومين عشان تحولها الى دتو
            CreateMap<RestaurantsEntity, RestaurantDto>()
               .ForMember(dest => dest.City , opt => opt.MapFrom(src =>src.address==null ? "no address found" : src.address.City))
               .ForMember (dest => dest.Street , opt => opt.MapFrom(src =>src.address==null ? "no street address found" : src.address.Street))
               .ForMember (dest => dest.PostalCode , opt => opt.MapFrom(src =>src.address==null ? "no postal code found" : src.address.PostalCode))
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.Dishes));


            //source -> target
            CreateMap<CreateRestaurantCommand, RestaurantsEntity>()
                .ForMember (dest=>dest.address ,opt=> opt.MapFrom(src =>new Address(){
                    City=src.City,
                    Street=src.Street,
                    PostalCode=src.PostalCode
                }));
            //source -> target
            CreateMap<UpdateRestaurantCommand , RestaurantsEntity>();


        }
    }
}
