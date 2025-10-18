using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator <UpdateRestaurantCommand>
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
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(dto=>dto.Name).MaximumLength(100).WithMessage("do not Exceed the limit")
                                  .MinimumLength(3).WithMessage("at least 3 characters");
            
            RuleFor(dto=>dto.Category).NotEmpty().WithMessage("Enter a valid category");
            RuleFor(dto=>dto.Category).Must(category=>_allowedCategories.Contains(category))
                                     .WithMessage($"Category must be one of the following: {string.Join(", ", _allowedCategories)}");
        }
    }
}
