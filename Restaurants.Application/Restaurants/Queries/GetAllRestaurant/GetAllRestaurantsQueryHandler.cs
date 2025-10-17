using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;

public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, List<RestaurantDto>>
{
    //we need an interfaces
    private readonly IRestaurantRepository _restaurantRepository;
    //We need Logging
    private readonly ILogger<GetAllRestaurantsQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllRestaurantsQueryHandler(IRestaurantRepository restaurantRepository, ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("We are getting some restaurants");
        var restaurant = await _restaurantRepository.GetAllAsync();

        var restaurantDtos =  _mapper.Map<IEnumerable<RestaurantDto>>(restaurant).ToList();

        return restaurantDtos;
    }
}
