using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories;

public interface IRestaurantRepository
{
    public Task<IEnumerable<RestaurantsEntity>> GetAllAsync();
    public Task<(IEnumerable<RestaurantsEntity>, int)> GetAllMatchingAsync(string searchPhrase,int pageNumber,int pageSize);
    public Task<RestaurantsEntity> GetByIdAsync(int id);
    public Task<RestaurantsEntity> CreateRestaurantAsync(RestaurantsEntity restaurant);
    public Task DeleteRestaurantAsync(RestaurantsEntity entity);
    public Task SaveChangesAsync();
}
