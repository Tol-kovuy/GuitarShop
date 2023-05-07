
using GuitarShop.DAL.Entities;

public class CartItem
{
    public long Id { get; set; }
    public long CartId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDate { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
}