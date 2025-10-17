using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantValidator : AbstractValidator<CreateRestaurantDto>
{
    public CreateRestaurantValidator()
    {
        RuleFor(dto=>dto.Name).MaximumLength(100).WithMessage("do not Exceed the limit")
                              .MinimumLength(3).WithMessage("at least 3 characters");
        
        RuleFor(dto=>dto.Category).NotEmpty().WithMessage("Enter a valid category");

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
