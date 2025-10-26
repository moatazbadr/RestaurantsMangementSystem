using MediatR;

namespace Restaurants.Application.Dishs.Commands.DeleteDishes
{
    public class DeleteDishesForRestaurantCommand : IRequest
    {
        public int RestaurantId { get; set; }
    }
}
