using MediatR;

namespace Restaurants.Application.User.Commands.AssignUserRole;

public class AssignUserRoleCommand :IRequest 
{
    public string UserEmail { get; set; } = null!;
    public string RoleName { get; set; } = null!;

}
