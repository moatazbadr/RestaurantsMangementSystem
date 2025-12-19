using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurants.Application.User;
using System.Threading.Tasks;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.RestaurantCommand.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommands_returnsRestaurantDto()
     {
        //arrange 
        //mocks
        var IloggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var IrestaurantRepositoryMock = new Mock<IRestaurantRepository>();
        IrestaurantRepositoryMock.Setup(repo=> repo.CreateRestaurantAsync(It.IsAny<RestaurantsEntity>()))
            .ReturnsAsync(new RestaurantsEntity { Id = 1, Name = "Test Restaurant" });


        var ImapperMock = new Mock<IMapper>();
        var command = new CreateRestaurantCommand();
        var restaurant = new RestaurantsEntity { Id = 1, Name = "Test Restaurant" };
        ImapperMock.Setup(m => m.Map<RestaurantsEntity>(command)).Returns(restaurant);
        
        var IuserContextMock = new Mock<IUserContext>();

        var user = new CurrentUser("userId-123", "Example@test.com", [], null, null);

        IuserContextMock.Setup(uc=>uc.GetCurrentUser()).Returns(user);
        var commandHandler = new CreateRestaurantCommandHandler(IrestaurantRepositoryMock.Object, IloggerMock.Object, ImapperMock.Object, IuserContextMock.Object);



        //act
        var result = await commandHandler.Handle(command, CancellationToken.None);



        //assert
        result.Should().NotBeNull();
        restaurant.ownerId.Should().Be(user.UserId);
    }
}