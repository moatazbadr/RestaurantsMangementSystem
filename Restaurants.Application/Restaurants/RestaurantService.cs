using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantService : IRestaurantService
{
    //we need an interface 
    private readonly IRestaurantRepository _restaurantRepository;
    //We need Logging
    private readonly ILogger<RestaurantService> _logger;

    public RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<RestaurantsEntity>> GetAll()
    {
        _logger.LogInformation("We are getting some restaurants");
        var restaurant = await _restaurantRepository.GetAllAsync();
        return restaurant;
    }

}
