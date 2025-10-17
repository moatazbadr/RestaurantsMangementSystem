using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    //we need an interfaces
    private readonly IRestaurantRepository _restaurantRepository;
    //We need Logging
    private readonly ILogger<GetRestaurantByIdQueryHandler> _logger;
    private readonly IMapper _mapper;
    public GetRestaurantByIdQueryHandler(IRestaurantRepository restaurantRepository, ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("getting a restaurant");
        var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
        var restaurantDto = _mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDto;
    }
}
