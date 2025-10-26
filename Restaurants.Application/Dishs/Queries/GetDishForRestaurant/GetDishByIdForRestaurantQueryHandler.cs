using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Dishs.Dtos;

namespace Restaurants.Application.Dishs.Queries.GetDishForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDishByIdForRestaurantQuery> _logger;
        
        public GetDishByIdForRestaurantQueryHandler(IDishRepository dishRepository, IRestaurantRepository restaurantRepository, IMapper mapper, ILogger<GetDishByIdForRestaurantQuery> logger)
        {
            _dishRepository = dishRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Fetching a Dish with Id : {DishId} for a Restaurant with Id : {RestaurantId}", request.DishId, request.RestaurantId);
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
                          
            if (restaurant == null)
            {
                _logger.LogWarning("Restaurant with id {RestaurantId} not found.", request.RestaurantId);
                throw new NotFoundException($"Restaurant with id {request.RestaurantId} not found.");
            }
            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
            if (dish == null)
            {
                _logger.LogWarning("Dish with id {DishId} not found in Restaurant with id {RestaurantId}.", request.DishId, request.RestaurantId);
                throw new NotFoundException($"Dish with id {request.DishId} not found in Restaurant with id {request.RestaurantId}.");
            }
            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;
        }
    }
}
