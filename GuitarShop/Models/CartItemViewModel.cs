namespace GuitarShop.Models;

public class CartItemViewModel
{
    public long Id { get; set; }
    public long CartId { get; set; }
    public ProductViewModel Product { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}