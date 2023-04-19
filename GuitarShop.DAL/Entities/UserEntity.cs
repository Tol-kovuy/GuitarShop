using GuitarShop.DAL.Enum;
using System.ComponentModel.DataAnnotations;

namespace GuitarShop.DAL.Entities;

public class UserEntity
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public CartEntity CartEntity { get; set; }
}


