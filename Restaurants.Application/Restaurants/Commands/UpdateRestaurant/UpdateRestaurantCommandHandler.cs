using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler :IRequestHandler<UpdateRestaurantCommand>

{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper ;

    public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateRestaurantCommand for Restaurant Id: {RestaurantId} with {Restaurant}", request.Id ,request);
        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
        if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {request.Id} not found.");
      
        
        _mapper.Map(request, restaurant); // map the updated fields from the command to the entity


        await _restaurantRepository.SaveChangesAsync();
        _logger.LogInformation("Successfully updated Restaurant Id: {RestaurantId}", request.Id);
      


    }
}
