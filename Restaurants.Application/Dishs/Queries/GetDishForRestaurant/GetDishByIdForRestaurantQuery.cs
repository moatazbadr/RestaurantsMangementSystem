using MediatR;
using Restaurants.Application.Dishs.Dtos;

namespace Restaurants.Application.Dishs.Queries.GetDishForRestaurant;

public class GetDishByIdForRestaurantQuery :IRequest<DishDto>
{
    public int RestaurantId { get; set; }
    public int DishId { get; set; }
}
