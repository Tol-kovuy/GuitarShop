using GuitarShop.DAL.Entities;
using GuitarShop.DAL.Enum;

namespace GuitarShop.Models;

public class UserViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public RoleType Role { get; set; }
    public IList<UserViewModel> Users { get; set; }
}
