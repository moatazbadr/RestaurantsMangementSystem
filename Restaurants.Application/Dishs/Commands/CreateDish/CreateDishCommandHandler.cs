using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurants.Application.Dishs.Commands.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand>
    {
        private readonly ILogger<CreateDishCommandHandler> _logger;
        private readonly IDishRepository _dishRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger
            , IMapper mapper
            , IDishRepository dishRepository, IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            _dishRepository = dishRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }
        public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {

            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantsEntityId);
            if (restaurant == null)
            {
                throw new NotFoundException($"Restaurant with Id : {request.RestaurantsEntityId} not found for this dish");
            }


           
            var createdDish = _mapper.Map<Dish>(request);

            await _dishRepository.CreateDishAsync(createdDish);
            _logger.LogInformation("Dish created: {DishName}", createdDish.Name);

        }
    }
}
