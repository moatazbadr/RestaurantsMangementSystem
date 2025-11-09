using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly RestaurantsDbContext _dbContext;

    public RestaurantRepository(RestaurantsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RestaurantsEntity> CreateRestaurantAsync(RestaurantsEntity restaurant)
    {
        _dbContext.restaurants.Add(restaurant);
        await _dbContext.SaveChangesAsync();
        return restaurant;
    }

    

    public async Task<IEnumerable<RestaurantsEntity>> GetAllAsync()
    {
        var Restaurants = await _dbContext.restaurants.Include(r=>r.Dishes).ToListAsync();
        return Restaurants;
    }
    public async Task<(IEnumerable<RestaurantsEntity>,int)> GetAllMatchingAsync(string? searchPhrase, int pageNumber, int pageSize, string? SortBy, SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var query = _dbContext.restaurants
            .Include(r => r.Dishes)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchPhraseLower))
        {
            query = query.Where(r =>
                (r.Name != null && r.Name.ToLower().Contains(searchPhraseLower)) ||
                (r.Description != null && r.Description.ToLower().Contains(searchPhraseLower))
            );
        }
        var totalCount = await query.CountAsync();
        if (SortBy is not null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<RestaurantsEntity, object>>>()
            {
                {nameof(RestaurantsEntity.Name),r=>r.Name},
                {nameof(RestaurantsEntity.Description),r=>r.Description },
                {nameof(RestaurantsEntity.Category),r=>r.Category }
            };
            var selectedColumn = columnSelector[SortBy];
            query = sortDirection == SortDirection.Ascending ?
                query.OrderBy(selectedColumn) : query.OrderByDescending(selectedColumn);

         }

        var restaurants = await query 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (restaurants,totalCount) ;
    }
    public async Task<RestaurantsEntity> GetByIdAsync(int id)
    {
        var restaurant = await _dbContext.restaurants.Include(d=>d.Dishes)
                                                     .FirstOrDefaultAsync(r=>r.Id==id);
       if (restaurant is not null)
        return restaurant;
        return null;
    }
    public async Task DeleteRestaurantAsync(RestaurantsEntity entity)
    {
       
        _dbContext.restaurants.Remove(entity);
        await _dbContext.SaveChangesAsync();

    }

    public async Task SaveChangesAsync()
    => await _dbContext.SaveChangesAsync();
}
