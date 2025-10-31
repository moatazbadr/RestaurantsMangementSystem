using Microsoft.OpenApi.Models;

namespace Restaurants.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
       builder.Services.AddAuthentication();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"

            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement() //key value pair
    {
        {
            new OpenApiSecurityScheme
            {
                 Reference = new OpenApiReference {
                    Type= ReferenceType.SecurityScheme ,
                    Id= "BearerAuth"
                 }
            }
            ,
            new List<string>()
        }
    }
            );
        }

);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddScoped<ErrorMiddleWare>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
        builder.Host.UseSerilog((context, config) => {
            config.ReadFrom.Configuration(context.Configuration);
        });


    }

}
