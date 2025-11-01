using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;

namespace Restaurants.Infrastructure.Authorization.Requirement;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var user =userContext.GetCurrentUser();
        logger.LogInformation("Handling MinimumAgeRequirement for user {UserEmail} with minimum age {MinimumAge}", user.Email, requirement.MinimumAge);

        if (user.DateOfBirth == null)
        {
            logger.LogWarning("No user context available. Failing MinimumAgeRequirement.");
            context.Fail();
            return Task.CompletedTask;
        }
        if (user.DateOfBirth.Value.AddYears(requirement.MinimumAge) < DateOnly.FromDateTime(DateTime.Now))
        {
            logger.LogInformation("User {UserEmail} meets the minimum age requirement of {MinimumAge}", user.Email, requirement.MinimumAge);
            context.Succeed(requirement);

        }
        else
        {
            logger.LogWarning("User {UserEmail} does not meet the minimum age requirement of {MinimumAge}", user.Email, requirement.MinimumAge);
            context.Fail();
        }
        return Task.CompletedTask;

    }
}
