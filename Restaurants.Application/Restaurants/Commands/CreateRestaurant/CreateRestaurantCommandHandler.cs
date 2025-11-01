using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.User;

namespace Restaurants.Application.Restaurants.Commands.RestaurantCommand;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, RestaurantDto>
{

    private readonly IRestaurantRepository _restaurantRepository;
    //We need Logging
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IUserContext userContext)
    {
        _userContext = userContext;
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }


    // Implementation goes here
    public async Task<RestaurantDto> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user =_userContext.GetCurrentUser();

        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }
        _logger.LogInformation("Creating a restaurant....{@Restaurant}",request);
     
        var restaurantToCreate = _mapper.Map<RestaurantsEntity>(request);
        restaurantToCreate.ownerId = user.UserId;
        var CreatedRestaurant = await _restaurantRepository.CreateRestaurantAsync(restaurantToCreate);
        var CreatedRestaurantDto = _mapper.Map<RestaurantDto>(CreatedRestaurant);
        return CreatedRestaurantDto;
    }
}
