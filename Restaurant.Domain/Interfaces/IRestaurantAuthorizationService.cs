using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Interfaces;

public interface IRestaurantAuthorizationService
{
    bool Authorize(RestaurantsEntity restaurant, RestaurantOperations ResourceOperation);
}