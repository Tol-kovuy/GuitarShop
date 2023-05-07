namespace GuitarShop.Models
{
    public class CartItemViewModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageData { get; set; }
    }
}