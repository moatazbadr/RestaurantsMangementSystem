
using Restaurants.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Services collection
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServicesExtension(builder.Configuration);
builder.Services.AddApplication();

#endregion

builder.Host.UseSerilog((context,config) => {
    config
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // will only log Warning and above from Microsoft namespaces
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information) // will log Information and above from EF Core namespaces
    .WriteTo.Console(outputTemplate : "[{Timestamp:dd:MM:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

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


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
