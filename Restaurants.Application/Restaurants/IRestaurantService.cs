using Restaurant.Domain.Entities;

namespace Restaurants.Application.Restaurants;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantsEntity>> GetAll();
    Task<RestaurantsEntity> GetById(int id);
}