using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Dishs.Commands.DeleteDishes
{
    public class DeleteDishesForRestaurantCommandHandler(
        IDishRepository _dishRepository,
        IRestaurantRepository _restaurantRepository,
        ILogger<DeleteDishesForRestaurantCommandHandler> _logger,
        IRestaurantAuthorizationService _restaurantAuthorizationService
        ) : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
    

        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting deletion of dishes for restaurant with ID {RestaurantId}", request.RestaurantId);
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                _logger.LogWarning("Restaurant with ID {RestaurantId} not found", request.RestaurantId);
                throw new NotFoundException($"Restaurant with ID {request.RestaurantId} not found. to delete all its dishes");
            }
            if (!_restaurantAuthorizationService.Authorize(restaurant, RestaurantOperations.delete))
            {
                _logger.LogWarning("Unauthorized attempt to delete Dishes for restaurant with ID: {RestaurantId}", request.RestaurantId);
                throw new ForbiddenExceptions("You do not have permission to delete this restaurant.");
            }
            await _dishRepository.DeleteAllAsync(restaurant.Dishes);
            


        }
    }
}
