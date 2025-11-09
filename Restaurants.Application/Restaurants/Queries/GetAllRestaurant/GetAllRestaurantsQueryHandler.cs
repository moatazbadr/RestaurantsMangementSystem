using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;

public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, PagesResults<RestaurantDto>>
{
    //we need an interfaces
    private readonly IRestaurantRepository _restaurantRepository;
    //We need Logging
    private readonly ILogger<GetAllRestaurantsQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllRestaurantsQueryHandler(IRestaurantRepository restaurantRepository, ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PagesResults<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("We are getting some restaurants");
      
        //if (string.IsNullOrEmpty(request.searchPhrase))
        //    {
        //    var allRestaurants = await _restaurantRepository.GetAllAsync();
        //    var allRestaurantDtos =  _mapper.Map<IEnumerable<RestaurantDto>>(allRestaurants).ToList();
        //    return allRestaurantDtos;
        //}
        var ( restaurant ,totalCount) = await _restaurantRepository.GetAllMatchingAsync(request.searchPhrase,request.pageNumber
            ,request.pageSize,
            request.sortBy,
          request.sortDirection
            );

        
        var restaurantDtos =  _mapper.Map<IEnumerable<RestaurantDto>>(restaurant).ToList();
        var pagesResults = new PagesResults<RestaurantDto>(restaurantDtos, totalCount, request.pageNumber, request.pageSize);

        return pagesResults;
    }
}
