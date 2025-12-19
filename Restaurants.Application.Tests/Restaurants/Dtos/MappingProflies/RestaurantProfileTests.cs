using AutoMapper;
using FluentAssertions;
using Restaurant.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Restaurants.Dtos.MappingProflies.Tests;

public class RestaurantProfileTests
{
    [Fact()]
    public void CreateMap_ForRestaurantToRestaurantDto_shouldMapCorrectly()
    {
        //arrange 
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<RestaurantProfile>();
        });

        var mapper = configuration.CreateMapper();


        //act

        var restaurant = new RestaurantsEntity()
        {
            Id = 1,
            Name = "Test",
            Description = "Test Description",
            Category = "Italian",
            HasDelivery = true,
            ContactEmail = "testRestaurant@gmail.com",
            ContactNumber = "0109765901",
            address = new Address { City = "giza", PostalCode = "19-900", Street = "talaat Harb" },
            //dishes properties is discarded
            
        };


        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);



        //assert

        restaurantDto.Should().NotBeNull();
        restaurant.Id.Should().Be(1);
        restaurant.Name.Should().Be(restaurant.Name);
        



    }
}