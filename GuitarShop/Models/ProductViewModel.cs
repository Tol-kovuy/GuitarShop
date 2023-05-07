namespace GuitarShop.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string FileName { get; set; }
    public IFormFile ImageData { get; set; }
}
