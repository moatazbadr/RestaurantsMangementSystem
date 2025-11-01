using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirement;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
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
        services.AddIdentityApiEndpoints<User>()
           .AddRoles<IdentityRole>()
           .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantsDbContext>();

        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IUserRoleSeeder, UserRoleSeeder>();
        services.AddAuthorizationBuilder().AddPolicy(validPolices.HasNationality, buider => buider.RequireClaim("Nationality"))
            .AddPolicy(validPolices.IsAtLeast20, buider => buider.AddRequirements(new MinimumAgeRequirement(20)))
            ;
        //added nationality policy required claim
        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
         
    }
}
