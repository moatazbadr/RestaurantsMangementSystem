using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;
using Restaurants.Application.User;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(RestaurantsEntity restaurant, RestaurantOperations ResourceOperation)
    {
        var user = userContext.GetCurrentUser();
        if (user is null)
        {
            logger.LogWarning("Authorization failed: No current user found for operation {Operation} on restaurant {RestaurantId}", ResourceOperation, restaurant.Name);
            return false;
        }

        logger.LogInformation("Authorizing user {email} for operation {Operation} on restaurant {RestaurantId}", user.Email, ResourceOperation, restaurant.Name);

        if (ResourceOperation == RestaurantOperations.create || ResourceOperation == RestaurantOperations.read)
        {
            logger.LogInformation("Create and Read are authorized for the user/owner with {email}", user.Email);
            return true;

        }
        if (ResourceOperation == RestaurantOperations.delete && user.IsInRole(ValidUserRoles.AdminRole))
        {
            logger.LogInformation("Delete operation is authorized for admin user with {email}", user.Email);
            return true;
        }
        if ((ResourceOperation == RestaurantOperations.delete || ResourceOperation == RestaurantOperations.update) && (user.UserId == restaurant.ownerId))
        {
            logger.LogInformation("Update and Delete operations are authorized for the restaurant owner with {email}", user.Email);
            return true;
        }
        return false;


    }
}
