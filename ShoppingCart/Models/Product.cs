using System.ComponentModel;

namespace ShoppingCart.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }
        [DisplayName("Photo")]
        public string? ImgUrl { get; set; }
    }
}
