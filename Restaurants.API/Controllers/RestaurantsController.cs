using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Domain.Entities;
using Restaurants.Application.Restaurants;

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
    [ProducesResponseType(typeof(RestaurantsEntity) ,200)]
    public async Task<IActionResult> getRestaurants()
    {
        var Restaurants = await _restaurantService.GetAll();
        return Ok(Restaurants);
    }

}
