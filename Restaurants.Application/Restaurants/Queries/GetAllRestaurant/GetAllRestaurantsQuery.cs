using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant
{
    public class GetAllRestaurantsQuery :IRequest <List<RestaurantDto>>
    {
        //طبعا هو كدا عشان مفيش في حاجه تتبعت مع الريكوست
    }
}
