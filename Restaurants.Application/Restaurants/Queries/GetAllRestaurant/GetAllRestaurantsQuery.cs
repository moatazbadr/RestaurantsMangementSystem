using MediatR;
using Restaurant.Domain.Constants;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant
{
    public class GetAllRestaurantsQuery :IRequest <PagesResults<RestaurantDto>>
    {
        public string ? searchPhrase { get; set; }

        public int pageNumber { get; set; }

        public int pageSize { get; set; }

        public string ? sortBy { get; set; }
     
        public SortDirection sortDirection { get; set; }    

    }
}
