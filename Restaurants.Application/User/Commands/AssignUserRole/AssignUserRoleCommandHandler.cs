using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.AssignUserRole;

public class AssignUserRoleCommandHandler(
    ILogger<AssignUserRoleCommandHandler> _logger,
    UserManager<Restaurant.Domain.Entities.User> _userManager,
    RoleManager<IdentityRole> _roleManager
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

        var normalizedRoleName = request.RoleName.Trim().ToUpperInvariant();
        var roleExists = await _roleManager.Roles.FirstOrDefaultAsync(r => r.NormalizedName == normalizedRoleName);
        if (roleExists == null)
        {
            _logger.LogWarning("Role {RoleName} does not exist", request.RoleName);
            throw new NotFoundException($"Role {request.RoleName} does not exist");
        }

        var result = await _userManager.AddToRoleAsync(user, roleExists.Name!);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Failed to assign role {RoleName} to {UserEmail}: {Errors}", roleExists.Name, user.Email, errors);
            throw new ApplicationException($"Failed to assign role: {errors}");
        }

        _logger.LogInformation("Role {RoleName} successfully assigned to user {UserEmail}", roleExists.Name, user.Email);
    }
}
