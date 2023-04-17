using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarShop.DAL.Entities;

public class CartEntity
{
    [Key]
    public long Id { get; set; }
    [ForeignKey("UserEntity")]
    public long UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; }
    public IList<ProductEntity> ProductEntity { get; set; }
}
