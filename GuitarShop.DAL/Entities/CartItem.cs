using GuitarShop.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    public long Id { get; set; }
    public long CartId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDate { get; set; }
    
    //public long ProductId { get; set; }

    public virtual Product Product { get; set; }
}