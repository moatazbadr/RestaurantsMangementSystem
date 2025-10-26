using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories
{
    public interface IDishRepository
    {
        public Task<IEnumerable<Dish>> GetAllAsync();
        public Task<Dish?> GetByIdAsync(int id);
        public Task<Dish> CreateDishAsync(Dish DishEntity);
        public Task DeleteAsync(Dish dish);
        public Task SaveChangesAsync();
    }
}
