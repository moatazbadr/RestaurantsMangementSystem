using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.RestaurantCommand;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurant;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RestaurantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(RestaurantDto), 200)]
    public async Task<IActionResult> getRestaurants()
    {
        var Restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
        return Ok(Restaurants);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RestaurantDto), 200)]
    public async Task<IActionResult> getRestaurant(int id)
    {
        var Restaurant = await _mediator.Send(new GetRestaurantByIdQuery() { Id = id });
        
        return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(RestaurantDto), 201)]
    public async Task<IActionResult> createRestaurant([FromBody] CreateRestaurantCommand dto)
    {
        var createdRestaurant = await _mediator.Send(dto);
        return CreatedAtAction(nameof(getRestaurant), new { id = createdRestaurant.Id }, createdRestaurant);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> deleteRestaurant([FromRoute] int id)
    {

        // To be implemented
         await  _mediator.Send(new DeleteRestaurantCommand (id));
        

        return NoContent(); //does not require any further action
    }
    [HttpPatch("{id:int}")]

    public async Task<IActionResult> updateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantCommand dto)
    {
        if (id != dto.Id)
            return BadRequest("Id from route does not match id from body");
        dto.Id = id;
         await _mediator.Send(dto);
        
            
        return NoContent();

    }



}
