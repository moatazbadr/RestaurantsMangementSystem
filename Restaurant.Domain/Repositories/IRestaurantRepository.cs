using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories;

public interface IRestaurantRepository
{
    public Task<IEnumerable<RestaurantsEntity>> GetAllAsync();

}
