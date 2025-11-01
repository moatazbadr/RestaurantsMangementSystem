using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurants.Application.User.Commands.AssignUserRole;

namespace Restaurants.Application.User.Commands.UnAssignUserRole;

public class UnAssignUserRoleCommandHandler(ILogger<UnAssignUserRoleCommandHandler> _logger, UserManager<Restaurant.Domain.Entities.User>
    _userManager, RoleManager<IdentityRole> _roleManager
    ) : IRequestHandler<UnAssignUserRoleCommand>
{
    public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UnAssigning role {@request}",request);

        var user = await _userManager.FindByEmailAsync(request.UserEmail);
        if (user == null)
        {
            _logger.LogWarning("User with email {UserEmail} not found", request.UserEmail);
            throw new NotFoundException($"User with email {request.UserEmail} not found");
        }
        var roleExists = await _roleManager.FindByNameAsync(request.RoleName.ToLower());
        if (roleExists == null)
        {
            _logger.LogWarning("Role {RoleName} does not exist", request.RoleName);
            throw new NotFoundException($"Role {request.RoleName} does not exist");
        }
        await _userManager.RemoveFromRoleAsync(user, roleExists.Name!);
    }
}