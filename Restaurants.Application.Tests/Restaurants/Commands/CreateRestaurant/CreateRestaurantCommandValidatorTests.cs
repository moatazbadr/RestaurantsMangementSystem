using Restaurants.Application.Restaurants.Commands.RestaurantCommand;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.RestaurantCommand.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_shouldNotHaveValidationErrors()
    {
        // arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Category = "Italian",
            ContactEmail = "contact@Test.Restaurant.com",
            Description = "A lovely place to dine.",
            ContactNumber = "01234567890",
            PostalCode = "12-345",
            City = "Test City",
            Street = "123 Test St",
            HasDelivery = true

        };
        // act

        var validator = new CreateRestaurantCommandValidator();

        var result = validator.Validate(command);



        // assert
        result.IsValid.Should().BeTrue();
    }
    [Fact()]
    public void Validator_ForValidCommand_shouldHaveSomeValidationErrors()
    {
        // arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Category = "",
            ContactEmail = "contact",
            Description = "A lovely place to dine.",
            ContactNumber = "01234567890",
            PostalCode = "12-345",
            City = "Test City",
            Street = "123 Test St",
            HasDelivery = true

        };
        // act

        var validator = new CreateRestaurantCommandValidator();

        var result = validator.TestValidate(command);



        // assert
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);

    }

    
}