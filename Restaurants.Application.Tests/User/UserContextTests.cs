using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace Restaurants.Application.User.Tests;

public class UserContextTests
{
    [Fact()]
    public void GetCurrentUser_withAuthenticatedUser_shouldReturnUser()
    {
        //arrange
     var dateOfBirth = new DateOnly(1990, 1, 1);
        var httpcontextMock = new Mock<IHttpContextAccessor>();
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "user-123"),
            new Claim(ClaimTypes.Email, "zezobadr988@gmail.com"),
            new Claim(ClaimTypes.Role, "admin"),
            new Claim(ClaimTypes.Role, "restaurantowner"),
            new Claim("DateOfBirth" , dateOfBirth.ToString("yy-MM-dd")),
            new Claim("Nationality" ,"Egyptian" )

        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims,"test"));
        httpcontextMock.Setup(h => h.HttpContext).Returns(new DefaultHttpContext
        {
            User = user
        });

        var userContext = new UserContext(httpcontextMock.Object);




        //act 

     var currentUser=   userContext.GetCurrentUser();  

        //assert
        currentUser.Should().NotBeNull();   
        currentUser!.UserId.Should().Be("user-123");
        currentUser.Email.Should().Be("zezobadr988@gmail.com");
        currentUser.Roles.Should().Contain(new[] { "admin", "restaurantowner" });



    }
}