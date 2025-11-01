using MediatR;

namespace Restaurants.Application.User.Commands.UnAssignUserRole
{
    public class UnAssignUserRoleCommand :IRequest
    {
        public string UserEmail { get; set; } = null;
        public string RoleName { get; set; } = null;

    }
}
