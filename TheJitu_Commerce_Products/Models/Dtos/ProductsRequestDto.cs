using System.ComponentModel.DataAnnotations;

namespace TheJitu_Commerce_Products.Models.Dtos
{
    public class ProductsRequestDto
    {
        [Required]
        [MinLength(3)]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        public string ProductDescription { get; set; } = string.Empty;
        [Required]
        [Range(1, 100000)]
        public int ProductPrice { get; set; }
        [Required]
        [Range(1, 100000)]
        public int ProductQuantity { get; set; }
    }
}
