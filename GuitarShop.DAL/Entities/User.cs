using GuitarShop.DAL.Enum;
using System.ComponentModel.DataAnnotations;

namespace GuitarShop.DAL.Entities;

public class User
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public virtual Cart Cart { get; set; }
}


