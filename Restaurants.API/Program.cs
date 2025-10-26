


using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Services collection
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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

builder.Services.AddServicesExtension(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddScoped<ErrorMiddleWare>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
#endregion


builder.Host.UseSerilog((context,config) => {
    config.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();
#region Seeding data 
var scoped = app.Services.CreateScope();
var seeder = scoped.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.SeedData();

#endregion// Configure the HTTP request pipeline.

#region Swagger implementation


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 
#endregion

app.UseSerilogRequestLogging(); //will log all HTTP requests
app.MapGroup("api/Accounts").MapIdentityApi<User>();

app.UseMiddleware<ErrorMiddleWare>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
