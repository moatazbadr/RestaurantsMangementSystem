using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;

public class GetAllRestaurantValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    List<int> validPageSizes = [5, 10, 15, 30];
    List<string> validSortByValues = [nameof(RestaurantDto.Name), nameof(RestaurantDto.Category), nameof(RestaurantDto.Description)];
    List<string> validSortDirections = ["asc", "desc"];

    public GetAllRestaurantValidator()
    {
        RuleFor(x => x.pageNumber)
            .GreaterThan(0).LessThan(int.MaxValue).WithMessage("Page number must be greater than 0.");
        RuleFor(x => x.pageSize)
            .Custom((value, context) =>
            {
                if (!validPageSizes.Contains(value))
                {
                    context.AddFailure($"Page size must be one of the following values: {string.Join(", ", validPageSizes)}.");
                }
            });
        RuleFor(x => x.sortBy)
            .Custom((value, context) =>
            {
                if (value != null && !validSortByValues.Contains(value))
                {
                    context.AddFailure($"Sort by must be one of the following values: {string.Join(", ", validSortByValues)}.");
                }
            });
       

    }
}
