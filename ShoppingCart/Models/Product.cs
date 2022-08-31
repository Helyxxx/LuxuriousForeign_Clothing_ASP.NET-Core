using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string? Description { get; set; }

        [DisplayName("Photo")]
        public string? ImgUrl { get; set; }
    }
}
