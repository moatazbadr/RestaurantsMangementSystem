using Restaurant.Domain.Entities;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> GetAll();
    Task<RestaurantDto> GetById(int id);
} 