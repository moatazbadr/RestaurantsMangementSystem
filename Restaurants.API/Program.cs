
using Restaurants.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Services collection
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServicesExtension(builder.Configuration);

#endregion

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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
