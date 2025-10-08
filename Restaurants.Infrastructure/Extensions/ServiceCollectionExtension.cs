using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddServicesExtension(this IServiceCollection services ,IConfiguration configuration)
    {
        services.AddDbContext<RestaurantsDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
    }
}
