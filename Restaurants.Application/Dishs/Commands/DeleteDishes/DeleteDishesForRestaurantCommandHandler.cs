using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Dishs.Commands.DeleteDishes
{
    public class DeleteDishesForRestaurantCommandHandler : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDishesForRestaurantCommandHandler> _logger;

        public DeleteDishesForRestaurantCommandHandler(IDishRepository dishRepository, IRestaurantRepository restaurantRepository, IMapper mapper, ILogger<DeleteDishesForRestaurantCommandHandler> logger)
        {
            _dishRepository = dishRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting deletion of dishes for restaurant with ID {RestaurantId}", request.RestaurantId);
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                _logger.LogWarning("Restaurant with ID {RestaurantId} not found", request.RestaurantId);
                throw new NotFoundException($"Restaurant with ID {request.RestaurantId} not found. to delete all its dishes");
            }
            await _dishRepository.DeleteAllAsync(restaurant.Dishes);
            


        }
    }
}
