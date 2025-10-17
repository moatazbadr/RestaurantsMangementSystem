using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Entities;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.RestaurantCommand;
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
    [ProducesResponseType(typeof(RestaurantDto) ,200)]
    public async Task<IActionResult> getRestaurants()
    {
        var Restaurants = await _mediator.Send(new GetAllRestaurantsQuery());
        return Ok(Restaurants);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RestaurantDto),200)]
    public async Task<IActionResult> getRestaurant(int id)
    {
        var Restaurant = await _mediator.Send(new GetRestaurantByIdQuery() { Id=id});
        if (Restaurant is not null)
            return Ok(Restaurant);
        return NotFound();
    }
    [HttpPost]
    [ProducesResponseType(typeof(RestaurantDto), 201)]
    public async Task<IActionResult> createRestaurant([FromBody] CreateRestaurantCommand dto)
    {
        var createdRestaurant = await _mediator.Send(dto);
        return CreatedAtAction(nameof(getRestaurant), new { id = createdRestaurant.Id }, createdRestaurant);
                               
    }

}
