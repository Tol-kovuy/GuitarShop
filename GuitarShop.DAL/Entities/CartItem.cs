using GuitarShop.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual Product Product { get; set; }
}