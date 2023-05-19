namespace GuitarShop.Models;

public class CartViewModel
{
    public int Id { get; set; }
    public IEnumerable<CartItemViewModel> CartItems { get; set; }
    public decimal TotalPrice { get; set; }
    public int ProductCounter { get; set; }
}
