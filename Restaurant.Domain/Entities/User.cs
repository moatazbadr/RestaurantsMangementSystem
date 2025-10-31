using Microsoft.AspNetCore.Identity;

namespace Restaurant.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly ? DateOfbirth { get; set; }
    public string ? Nationality { get; set; }


}
