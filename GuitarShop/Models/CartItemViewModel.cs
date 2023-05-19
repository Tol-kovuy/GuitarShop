namespace GuitarShop.Models;

public class CartItemViewModel
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public ProductViewModel Product { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}