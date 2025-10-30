

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Services collection



builder.Services.AddServicesExtension(builder.Configuration);
builder.Services.AddApplication();

#endregion



builder.AddPresentation();

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
