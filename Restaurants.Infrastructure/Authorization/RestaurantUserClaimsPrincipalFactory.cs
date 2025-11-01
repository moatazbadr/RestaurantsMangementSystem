using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurant.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorization;

public class RestaurantUserClaimsPrincipalFactory(UserManager<User> userManager, 
    RoleManager<IdentityRole> roleManager, 
    IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<User, IdentityRole>(userManager, roleManager, options)
{
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id =  await GenerateClaimsAsync(user);

        if (!string.IsNullOrEmpty(user.Nationality))
            id.AddClaim(new Claim("Nationality", user.Nationality)); //add custom claim to the token
        if (user.DateOfbirth == null) 
            id.AddClaim(new Claim("DateOfBirth", user.DateOfbirth!.Value.ToString("yy-mm-dd")));

        return new ClaimsPrincipal (id);
    }

}
