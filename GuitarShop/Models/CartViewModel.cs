namespace GuitarShop.Models;

public class CartViewModel
{
    public long Id { get; set; }
    public IEnumerable<CartItemViewModel> CartItems { get; set; }
    public decimal TotalPrice { get; set; }
}
