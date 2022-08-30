using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class Cart
    {
        public int CartID { get; set; }

        //[Key]
        public string UserID { get; set; }

        //[Key]
        public int ProductID { get; set; }

        public int Quantity { get; set; }
        public bool Paid { get; set; }
        public Product Product { get; set; }
    }
}
