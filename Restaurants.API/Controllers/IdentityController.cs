using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        [HttpPatch]
        public async Task<IActionResult>UpdateUserDetails()
        {
            // Implementation for updating user details goes here.
            return Ok();
        }

    }
}
