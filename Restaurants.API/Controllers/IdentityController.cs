using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Constants;
using Restaurants.Application.User.Commands.AssignUserRole;
using Restaurants.Application.User.Commands.UnAssignUserRole;
using Restaurants.Application.User.Commands.UpdateUserDetails;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly IMediator _mediator;
        public IdentityController(ILogger<IdentityController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        
        [HttpPatch("User")]
        [Authorize]
        public async Task<IActionResult>UpdateUserDetails(UpdateUserDetailsCommand userDetailsCommand)
        {
            await _mediator.Send(userDetailsCommand);

            return NoContent();
        }
        [HttpPost("UserRole")]
        [Authorize(Roles = ValidUserRoles.AdminRole)]
        public async Task<IActionResult>  AssignUserRole(AssignUserRoleCommand assignUserRoleCommand)
        {
            await _mediator.Send(assignUserRoleCommand);
            return NoContent();
        }
        [HttpDelete("UserRole")]
        [Authorize(Roles = ValidUserRoles.AdminRole)]
        public async Task<IActionResult> UnAssignUserRole(UnAssignUserRoleCommand UnassignUserRoleCommand)
        {
            await _mediator.Send(UnassignUserRoleCommand);
            return NoContent();
        }

    }
}
