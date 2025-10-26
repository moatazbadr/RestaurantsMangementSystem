using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class DishRepository(RestaurantsDbContext _context) : IDishRepository
{


    public async Task<Dish> CreateDishAsync(Dish DishEntity)
    {
        await _context.dishes.AddAsync(DishEntity);
        
        await _context.SaveChangesAsync();
        
        return DishEntity;


    }

    public Task DeleteAsync(Dish dish)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Dish>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Dish?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
