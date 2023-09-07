using System.ComponentModel.DataAnnotations;

namespace TheJitu_Commerce_Products.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public int ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
    }
}
