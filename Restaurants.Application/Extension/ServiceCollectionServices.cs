using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extension;

public static class ServiceCollectionServices
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionServices).Assembly;
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly)
            .AddFluentValidationAutoValidation()
            ;
        
    }
}
