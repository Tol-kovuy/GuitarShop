using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuitarShop.DAL.Entities;

public class CartEntity
{
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public long UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; }
    public IList<ProductEntity> ProductEntities { get; set; }
}
