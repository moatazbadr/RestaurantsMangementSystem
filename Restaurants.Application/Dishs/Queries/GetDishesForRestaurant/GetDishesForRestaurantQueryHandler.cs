using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Dishs.Dtos;

namespace Restaurants.Application.Dishs.Queries.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQueryHandler : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;   
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<GetDishesForRestaurantQuery> _logger;

        public GetDishesForRestaurantQueryHandler(IDishRepository dishRepository, IMapper mapper, IRestaurantRepository restaurantRepository, ILogger<GetDishesForRestaurantQuery> logger)
        {
            _dishRepository = dishRepository;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetDishesForRestaurantQuery for RestaurantId: {RestaurantId}", request.RestaurantId);
            var Restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
           
            if (Restaurant == null)
            {
                _logger.LogWarning("Restaurant with ID {RestaurantId} not found.", request.RestaurantId);
                throw new NotFoundException($"no Restaurant Was found with the Id : {request.RestaurantId}");
            }
            var MappedDishes = _mapper.Map<IEnumerable<DishDto>>(Restaurant.Dishes);
            _logger.LogInformation("Retrieved {DishCount} dishes for RestaurantId: {RestaurantId}", MappedDishes.Count(), request.RestaurantId);
            return MappedDishes;



        }
    }
}
