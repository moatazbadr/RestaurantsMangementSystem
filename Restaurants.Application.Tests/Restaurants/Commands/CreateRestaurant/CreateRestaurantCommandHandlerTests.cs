using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurants.Application.User;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.RestaurantCommand.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public void Handle_ForValidCommands_returnsRestaurantDto()
    {
        //arrange 
        //mocks
        var IloggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
        var IrestaurantRepositoryMock = new Mock<IRestaurantRepository>();
        IrestaurantRepositoryMock.Setup(repo=> repo.CreateRestaurantAsync(It.IsAny<RestaurantsEntity>()))
            .ReturnsAsync(new RestaurantsEntity { Id = 1, Name = "Test Restaurant" });
        var ImapperMock = new Mock<IMapper>();
        var IuserContextMock = new Mock<IUserContext>();

        var user = new CurrentUser("userId-123", "Example@test.com", [], null, null);

        IuserContextMock.Setup(uc=>uc.GetCurrentUser()).Returns(user);
        var commandHandler = new CreateRestaurantCommandHandler(IrestaurantRepositoryMock.Object, IloggerMock.Object, ImapperMock.Object, IuserContextMock.Object);



        //act




        //assert

    }
}