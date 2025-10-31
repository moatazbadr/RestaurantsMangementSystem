using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.UpdateUserDetails;

public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
{
    private readonly ILogger<UpdateUserDetailsCommandHandler> _logger;
    private readonly IUserContext _Usercontext;
    private readonly IUserStore<Restaurant.Domain.Entities.User> _userStore;
    public UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,IUserContext context,IUserStore<Restaurant.Domain.Entities.User>userStore)
    {
        _logger = logger;
        _userStore = userStore;
        _Usercontext = context;
    }
    public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = _Usercontext.GetCurrentUser();
        _logger.LogInformation("updating information for the user with {userId} , with{@request}",user!.UserId,request);
        var dbuser = await _userStore.FindByIdAsync(user.UserId,cancellationToken); //cancellation works incase of request timeout by client
        if (dbuser is  null)
        {
            throw new NotFoundException("No user was found");
        }
        dbuser.Nationality = request.Nationality;
        dbuser.DateOfbirth = request.DateOfbirth;
      await  _userStore.UpdateAsync(dbuser,cancellationToken);

    }
}
