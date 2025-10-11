using Restaurant.Domain.Entities;
using Restaurants.Application.Dishs.Dtos;

namespace Restaurants.Application.Restaurants.Dtos
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public List<DishDto> Dishes { get; set; } = new List<DishDto>();

        public static RestaurantDto FromEntity(RestaurantsEntity restaurant)
        {
            return new RestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                Category = restaurant.Category,
                HasDelivery = restaurant.HasDelivery,
                City = restaurant.address.City,
                Street = restaurant.address.Street,
                PostalCode = restaurant.address.PostalCode,
                Dishes = restaurant.Dishes?.Select(d => new DishDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Price = d.Price,
                    KiloCalories = d.KiloCalories
                }).ToList() ?? new List<DishDto>()
            };
        }
    }
}
