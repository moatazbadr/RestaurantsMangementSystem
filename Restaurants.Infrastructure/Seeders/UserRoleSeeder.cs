
using Microsoft.AspNetCore.Identity;
using Restaurant.Domain.Constants;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    public class UserRoleSeeder : IUserRoleSeeder
    {
        private readonly RestaurantsDbContext _dbContext;

        public UserRoleSeeder(RestaurantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (_dbContext == null) {
                throw new Exception();
            
            }
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                await _dbContext.SaveChangesAsync();
            }

        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> UserRoles  = 
                [
                new (ValidUserRoles.UserRole),
                new (ValidUserRoles.AdminRole),
                new (ValidUserRoles.RestaurantOwner)
                ];
            return UserRoles;
        }
    }
}
