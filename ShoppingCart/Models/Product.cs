namespace ShoppingCart.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl { get; set; }
    }
}
