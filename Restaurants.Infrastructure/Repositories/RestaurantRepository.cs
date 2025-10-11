using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly RestaurantsDbContext _dbContext;

    public RestaurantRepository(RestaurantsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<RestaurantsEntity>> GetAllAsync()
    {
        var Restaurants = await _dbContext.restaurants.Include(r=>r.Dishes).ToListAsync();
        return Restaurants;
    }

    public async Task<RestaurantsEntity> GetByIdAsync(int id)
    {
        var restaurant = await _dbContext.restaurants.FindAsync(id);
       if (restaurant is not null)
        return restaurant;
        return null;
    }
}
