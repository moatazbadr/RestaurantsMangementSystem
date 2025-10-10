using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

public class RestaurantService 
{
    //we need an interface 
    public async Task<IEnumerable<RestaurantsEntity>> GetAll()
    {
        var restaurant = new List<RestaurantsEntity>();
        return restaurant;
    }

}
