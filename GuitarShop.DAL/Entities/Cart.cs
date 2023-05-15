using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarShop.DAL.Entities;

public class Cart
{
    public long Id { get; set; }
    public long UserId { get; set; }
    
    public virtual ICollection<CartItem> CartItems { get; set; }
 
    [NotMapped]
    public int CountItems
    {
        get
        {
            var count = 0;
            foreach (var item in CartItems)
            {
                count += item.Quantity;
            }
            return count;
        }
    }

    [NotMapped]
    public decimal TotalPrice
    {
        get
        {
            var total = 0m;
            foreach (var cartItem in CartItems)
            {
                total += cartItem.Quantity * cartItem.Product.Price;
            }
            return total;
        }
    }
}
