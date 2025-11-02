using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(IRestaurantRepository _restaurantRepository, 
    ILogger<DeleteRestaurantCommandHandler> _logger,
    IRestaurantAuthorizationService _restaurantAuthorizationService
    
    ) : IRequestHandler<DeleteRestaurantCommand>
{
  
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
       _logger.LogInformation("Deleting restaurant with ID: {RestaurantId}", request.Id);
         var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
        
        if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {request.Id} not found.");   
        if (!_restaurantAuthorizationService.Authorize(restaurant, RestaurantOperations.delete))
        {
            _logger.LogWarning("Unauthorized attempt to delete restaurant with ID: {RestaurantId}", request.Id);
            throw new ForbiddenExceptions("You do not have permission to delete this restaurant.");
        }
        await _restaurantRepository.DeleteRestaurantAsync(restaurant);
        _logger.LogInformation("Successfully deleted restaurant with ID: {RestaurantId}", request.Id);
        
    }
}
