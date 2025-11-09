using FluentAssertions;
using Restaurant.Domain.Constants;
using Xunit;

namespace Restaurants.Application.User.Tests;

public class CurrentUserTests
{
    [Fact()]
    public void IsInRole_withMactingRole_shouldReturnTrue()
    {
        // Arrange
        var user = new CurrentUser(
            UserId: "user-123",
            Email: "zezobadr@gmail.com", [ValidUserRoles.AdminRole, ValidUserRoles.RestaurantOwner], null, null);

        // Act
        var result = user.IsInRole(ValidUserRoles.AdminRole);


        // Assert
       result.Should().BeTrue();

    }
    [Fact()]
    public void IsInRole_withNoMactingRole_shouldReturnFalse()
    {
        // Arrange
        var user = new CurrentUser(
            UserId: "user-123",
            Email: "zezobadr@gmail.com", [ValidUserRoles.AdminRole, ValidUserRoles.RestaurantOwner], null, null);

        // Act
        var result = user.IsInRole(ValidUserRoles.UserRole);


        // Assert
        result.Should().BeFalse();

    }
    [Fact()]
    public void IsInRole_withNoMactingRoleCase_shouldReturnFalse()
    {
        // Arrange
        var user = new CurrentUser(
            UserId: "user-123",
            Email: "zezobadr@gmail.com", [ValidUserRoles.AdminRole, ValidUserRoles.RestaurantOwner], null, null);

        // Act
        var result = user.IsInRole(ValidUserRoles.AdminRole.ToUpper());


        // Assert
        result.Should().BeFalse();

    }
}