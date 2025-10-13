using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants;

internal class RestaurantService : IRestaurantService
{
    //we need an interface 
    private readonly IRestaurantRepository _restaurantRepository;
    //We need Logging
    private readonly ILogger<RestaurantService> _logger;
    private readonly IMapper _mapper;

    public RestaurantService(IRestaurantRepository restaurantRepository, ILogger<RestaurantService> logger ,IMapper mapper)

    {
        _mapper = mapper;
        _restaurantRepository = restaurantRepository;
        _logger = logger;
    }

    public async Task<RestaurantDto> CreateAsync(CreateRestaurantDto dto)
    {
        _logger.LogInformation("Creating a restaurant....");
        var restaurantToCreate = _mapper.Map<RestaurantsEntity>(dto);
        var CreatedRestaurant = await _restaurantRepository.CreateRestaurantAsync(restaurantToCreate);
        var CreatedRestaurantDto = _mapper.Map<RestaurantDto>(CreatedRestaurant);
        return CreatedRestaurantDto;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAll()
    {
        _logger.LogInformation("We are getting some restaurants");
        var restaurant = await _restaurantRepository.GetAllAsync();

        var restaurantDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurant);

        return restaurantDtos;
    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        _logger.LogInformation("getting a restaurant");
        var restaurant = await _restaurantRepository.GetByIdAsync( id);
       var restaurantDto =  _mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDto;
    }

}
