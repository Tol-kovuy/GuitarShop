using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarShop.DAL.Entities;

public class Cart
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; }
}
