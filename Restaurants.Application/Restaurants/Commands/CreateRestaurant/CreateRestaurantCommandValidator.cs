using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.RestaurantCommand;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> _allowedCategories =
    [
        "Italian",
        "Chinese",
        "Mexican",
        "Indian",
        "French",
        "Japanese",
        "Mediterranean",
        "Thai",
        "American",
        "Spanish"
    ];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto=>dto.Name).MaximumLength(100).WithMessage("do not Exceed the limit")
                              .MinimumLength(3).WithMessage("at least 3 characters");
        
        RuleFor(dto=>dto.Category).NotEmpty().WithMessage("Enter a valid category");
        RuleFor(dto=>dto.Category).Must(category=>_allowedCategories.Contains(category))
                                 .WithMessage($"Category must be one of the following: {string.Join(", ", _allowedCategories)}");

        RuleFor(dto=>dto.ContactEmail).EmailAddress().When(dto=>!string.IsNullOrEmpty(dto.ContactEmail))
                                 .WithMessage("Please enter a valid email address");

        RuleFor(dto=>dto.Description).NotEmpty().WithMessage("Description cannot be empty");

        RuleFor(dto => dto.ContactNumber).Matches(@"^(\+201|01)[0125][0-9]{8}$")
            .WithMessage("Please Enter a valid Egyptian number")
            ;

        RuleFor(dto=>dto.PostalCode).Matches(@"^\d{2}-\d{3}$").When(dto=>!string.IsNullOrEmpty(dto.PostalCode))
                                 .WithMessage("Postal code must be in the format XX-XXX");
    }
}
