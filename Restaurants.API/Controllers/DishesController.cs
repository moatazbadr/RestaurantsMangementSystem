using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishs.Commands.CreateDish;
using Restaurants.Application.Dishs.Dtos;
using Restaurants.Application.Dishs.Queries.GetDishesForRestaurant;
using Restaurants.Application.Dishs.Queries.GetDishForRestaurant;

namespace Restaurants.API.Controllers;

[Route("api/restaurants/{restaurantId}/dishes")]
[ApiController]
public class DishesController : ControllerBase
{
    private readonly IMediator _mediator;
    public DishesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task <IActionResult> CreateDish([FromRoute]int restaurantId ,CreateDishCommand dishCommand)
    {
        dishCommand.RestaurantsEntityId = restaurantId;
        await  _mediator.Send(dishCommand);
        return Created();
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForARestaurant([FromRoute] int restaurantId)
    {
        var query = new GetDishesForRestaurantQuery
        {
            RestaurantId = restaurantId
        };

        var dishes = await _mediator.Send(query);

        return Ok(dishes);

    }
    [HttpGet("{dishId:int}")]
    public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
    {
        // To be implemented
        var query = new GetDishByIdForRestaurantQuery
        {
            RestaurantId = restaurantId,
            DishId = dishId
        };
        var dish = await _mediator.Send(query);
        return Ok(dish);
    }



}
