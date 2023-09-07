namespace TheJitu_Commerce_Cart.Models.Dtos
{
    public class CartHeaderDto
    {
        public Guid CartHeaderId { get; set; }
        public Guid UserId { get; set; }
        public string? CouponCode { get; set; }
        public double OrderTotal { get; set; }
        public int DiscountTotal { get; set; }
    }
}