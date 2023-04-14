namespace GuitarShop.DAL.Entities;

public class ProductEntity
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string ImageData { get; set; }
}
