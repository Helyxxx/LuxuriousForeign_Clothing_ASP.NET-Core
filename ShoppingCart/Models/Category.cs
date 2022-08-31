using System.ComponentModel;

namespace ShoppingCart.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        
        [DisplayName("Category")]
        public string CategoryName { get; set; }
    }
}
