using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
    public DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ILogger<DeleteRestaurantCommandHandler> logger)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
    }
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
       _logger.LogInformation("Deleting restaurant with ID: {RestaurantId}", request.Id);
         var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
        
        if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {request.Id} not found.");   

        await _restaurantRepository.DeleteRestaurantAsync(restaurant);
        _logger.LogInformation("Successfully deleted restaurant with ID: {RestaurantId}", request.Id);
        
    }
}
