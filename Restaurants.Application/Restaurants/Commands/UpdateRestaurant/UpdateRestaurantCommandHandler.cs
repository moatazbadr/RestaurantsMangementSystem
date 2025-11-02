using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IRestaurantRepository _restaurantRepository,
    ILogger<UpdateRestaurantCommandHandler> _logger,
    IMapper _mapper ,    IRestaurantAuthorizationService _restaurantAuthorizationService
    ) :IRequestHandler<UpdateRestaurantCommand>

{
    

    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateRestaurantCommand for Restaurant Id: {RestaurantId} with {Restaurant}", request.Id ,request);
        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {request.Id} not found.");

        if (!_restaurantAuthorizationService.Authorize(restaurant, RestaurantOperations.update))
        {
            _logger.LogWarning("Unauthorized attempt to delete restaurant with ID: {RestaurantId}", request.Id);
            throw new ForbiddenExceptions("You do not have permission to delete this restaurant.");
        }

        _mapper.Map(request, restaurant); // map the updated fields from the command to the entity


        await _restaurantRepository.SaveChangesAsync();
        _logger.LogInformation("Successfully updated Restaurant Id: {RestaurantId}", request.Id);
      


    }
}
