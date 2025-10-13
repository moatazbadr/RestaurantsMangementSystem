using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Entities;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(RestaurantDto) ,200)]
    public async Task<IActionResult> getRestaurants()
    {
        var Restaurants = await _restaurantService.GetAll();
        return Ok(Restaurants);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(RestaurantDto),200)]
    public async Task<IActionResult> getRestaurant(int id)
    {
        var Restaurant = await _restaurantService.GetById(id);
        if (Restaurant is not null)
            return Ok(Restaurant);
        return NotFound();
    }
    [HttpPost]
    [ProducesResponseType(typeof(RestaurantDto), 201)]
    public async Task<IActionResult> createRestaurant([FromBody] CreateRestaurantDto dto)
    {
        var createdRestaurant = await _restaurantService.CreateAsync(dto);
        return CreatedAtAction(nameof(getRestaurant), new { id = createdRestaurant.Id }, createdRestaurant);
                               
    }

}
