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
        var Restaurants = await _dbContext.restaurants.ToListAsync();
        return Restaurants;
    }
}
