﻿using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

public class RestaurantsDbContext : DbContext
{
    //انا غبي بس عديها معلش
    internal DbSet<RestaurantsEntity> restaurants { get; set; }
    internal DbSet<Dish> dishes { get; set; }

    public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> dbContextOptions):base(dbContextOptions)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<RestaurantsEntity>()
            .OwnsOne(r => r.address);

        modelBuilder.Entity<RestaurantsEntity>()
            .HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantsEntityId)
            ;



    }

}
