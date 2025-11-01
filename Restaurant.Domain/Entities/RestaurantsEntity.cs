namespace Restaurant.Domain.Entities;

public class RestaurantsEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    public string? ContactEmail { get; set; } 
    public string? ContactNumber { get; set; } 

    public Address? address { get; set; }
    public List<Dish> Dishes { get; set; } = new List<Dish>();

    public User owner { get; set; } = default!;
    public string ownerId { get; set; } = default!;


}
