using GuitarShop.DAL.Entities;

namespace GuitarShop.BLL.Models;

public class Cart
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public IList<Product> Product { get; set; }
}
