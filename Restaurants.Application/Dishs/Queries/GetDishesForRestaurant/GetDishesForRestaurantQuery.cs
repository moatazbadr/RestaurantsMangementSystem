using MediatR;
using Restaurants.Application.Dishs.Dtos;

namespace Restaurants.Application.Dishs.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQuery :IRequest<IEnumerable<DishDto>>
{
    public int RestaurantId { get; set; } 

}
