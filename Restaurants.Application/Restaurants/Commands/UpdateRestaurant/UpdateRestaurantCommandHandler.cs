using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler :IRequestHandler<UpdateRestaurantCommand,bool>

{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
    public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ILogger<UpdateRestaurantCommandHandler> logger)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
    }
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateRestaurantCommand for Restaurant Id: {RestaurantId}", request.Id);
        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
        {
            _logger.LogWarning("Restaurant with Id: {RestaurantId} not found", request.Id);
            return false;
        }
        // Update restaurant properties
        restaurant.Name = request.Name ?? restaurant.Name;
        restaurant.Description = request.Description ?? restaurant.Description;
        restaurant.Category = request.Category ?? restaurant.Category;
        restaurant.HasDelivery = request.HasDelivery;

        await _restaurantRepository.SaveChangesAsync();
        _logger.LogInformation("Successfully updated Restaurant Id: {RestaurantId}", request.Id);
        return true;


    }
}
