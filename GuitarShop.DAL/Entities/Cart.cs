namespace GuitarShop.DAL.Entities;

public class Cart
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; }
}
