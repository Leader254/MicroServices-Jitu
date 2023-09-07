using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheJitu_Commerce_Cart.Models.Dtos;

namespace TheJitu_Commerce_Cart.Models
{
    public class CartDetail
    {
        [Key]
        public Guid CartDetailId { get; set; }
        public Guid CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; set; }
        public Guid ProductId { get; set; }
        [NotMapped]
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
    }
}