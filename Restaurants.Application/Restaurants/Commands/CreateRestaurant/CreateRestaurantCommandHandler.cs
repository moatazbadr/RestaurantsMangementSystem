using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.RestaurantCommand;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, RestaurantDto>
{

    private readonly IRestaurantRepository _restaurantRepository;
    //We need Logging
    private readonly ILogger<CreateRestaurantCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }


    // Implementation goes here
    public async Task<RestaurantDto> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a restaurant....{@Restaurant}",request);
        var restaurantToCreate = _mapper.Map<RestaurantsEntity>(request);
        var CreatedRestaurant = await _restaurantRepository.CreateRestaurantAsync(restaurantToCreate);
        var CreatedRestaurantDto = _mapper.Map<RestaurantDto>(CreatedRestaurant);
        return CreatedRestaurantDto;
    }
}
