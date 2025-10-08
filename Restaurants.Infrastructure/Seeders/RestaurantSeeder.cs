using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

public class RestaurantSeeder : IRestaurantSeeder
{
    private readonly RestaurantsDbContext _dbContext;

    public RestaurantSeeder(RestaurantsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedData()
    {
        if (await _dbContext.Database.CanConnectAsync())
        {
            if (!_dbContext.restaurants.Any())
            {
                var Restaurants = GetRestaurants();
                await _dbContext.restaurants.AddRangeAsync(Restaurants);
                await _dbContext.SaveChangesAsync();
            }

        }
    }

    private IEnumerable<RestaurantsEntity> GetRestaurants()
    {
        List<RestaurantsEntity> restaurants = [
            new ()
                {
                Name="Bazooka",
                Category="Fast Food",
                Description ="A cheap and good alternative to the boycott brands also founded in Egypt",
                ContactEmail="info@bazookaegy.com",
                ContactNumber="16455",
                HasDelivery=true,
                Dishes =
                    [
                    new ()
                        {
                        Name="Family dinner box",
                        Description= "nashville hot 10 pieces",
                        Price =220M

                        },
                        new(){
                            Name="Single box meal",
                            Description ="5 pieces of fried chicken",
                            Price=145M
                        }

                    ]
                   ,address =new (){
                       City="giza",
                       PostalCode="1998",
                       Street="talaat harb st"
                   }

                },
            new ()
                {
                    Name="McDonald's",
                    Category="Fast Food",
                    Description ="American fast food company, founded in 1940 as a restaurant operated by Richard and Maurice McDonald, in San Bernardino, California, United States. They rechristened their business as a hamburger stand, and later turned the company into a franchise, with the Golden Arches logo being introduced in 1953 at a location in Phoenix, Arizona. In 1955, Ray Kroc, a businessman, joined the company as a franchise agent and proceeded to purchase the chain from the McDonald brothers. McDonald's had its original headquarters in Oak Brook, Illinois, but moved its global headquarters to Chicago in June 2018.",
                    ContactEmail=" contact@mc.com",
                    ContactNumber="16116",
                    HasDelivery=true,
                    Dishes =
                        [
                        new ()
                            {
                            Name="Big Mac",
                            Description= "Two 100% fresh beef patties, special sauce, lettuce, cheese, pickles, onions on a sesame seed bun",
                            Price =85M

                            },
                            new(){
                                Name="McChicken",
                                Description ="Crispy chicken fillet with mayonnaise and lettuce on a toasted bun",
                                Price=60M
                            }

                        ]
                       ,address =new (){
                           City="cairo",
                           PostalCode="1234",
                           Street="mcdonald st"
                       }


                }

            ];
        return restaurants;
    }
}
