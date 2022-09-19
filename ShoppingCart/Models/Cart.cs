using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public bool Paid { get; set; }
        public Product Product { get; set; }
    }
}
