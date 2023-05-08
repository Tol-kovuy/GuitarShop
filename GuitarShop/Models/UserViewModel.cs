using GuitarShop.DAL.Entities;
using GuitarShop.DAL.Enum;

namespace GuitarShop.Models;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public IList<UserViewModel> Users { get; set; }
}
