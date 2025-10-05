using Microsoft.EntityFrameworkCore;

namespace Restaurants.Infrastructure.Persistence;

public class RestaurantsDbContext : DbContext
{
    //انا غبي بس عديها معلش
    internal DbSet<Restaurant.Domain.Entities.Restaurant> restaurants { get; set; }

}
