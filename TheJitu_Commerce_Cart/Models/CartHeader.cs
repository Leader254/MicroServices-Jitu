using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheJitu_Commerce_Cart.Models
{
    public class CartHeader
    {
        [Key]
        public Guid CartHeaderId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string? CouponCode { get; set; } = string.Empty;
        [NotMapped]
        public int Discount { get; set; }
        [NotMapped]
        public int CartTotal { get; set; }
    }
}