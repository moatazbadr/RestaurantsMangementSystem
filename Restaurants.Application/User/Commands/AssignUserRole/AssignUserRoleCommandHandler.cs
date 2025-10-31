using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> _logger,UserManager<Restaurant.Domain.Entities.User>
    _userManager ,RoleManager<IdentityRole> _roleManager
    ) : IRequestHandler<AssignUserRoleCommand>
{
    public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Assigning role {RoleName} to user {UserEmail}", request.RoleName, request.UserEmail);
         
        var user = await _userManager.FindByEmailAsync(request.UserEmail);
        if (user == null)
        {
            _logger.LogWarning("User with email {UserEmail} not found", request.UserEmail);
            throw new NotFoundException($"User with email {request.UserEmail} not found");
        }
        var roleExists = await _roleManager.FindByNameAsync(request.RoleName.ToLower());
        if (roleExists==null)
        {
            _logger.LogWarning("Role {RoleName} does not exist", request.RoleName);
            throw new NotFoundException($"Role {request.RoleName} does not exist");
        }
         await _userManager.AddToRoleAsync(user, roleExists.Name!);



    }
}
