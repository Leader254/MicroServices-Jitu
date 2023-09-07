using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Cart.Context;
using TheJitu_Commerce_Cart.Models;
using TheJitu_Commerce_Cart.Models.Dtos;
using TheJitu_Commerce_Cart.Services.IService;

namespace TheJitu_Commerce_Cart.Services
{
    public class CartServices : ICartInterface
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICouponInterface _couponInterface;
        private readonly IProductInterface _productInterface;

        public CartServices(AppDbContext _db, IMapper mapper, ICouponInterface couponInterface, IProductInterface productInterface)
        {
            _context = _db;
            _mapper = mapper;
            _couponInterface = couponInterface;
            _productInterface = productInterface;
        }
        public async Task<bool> ApplyCoupon(CartDto cartDto)
        {
            CartHeader getCartHeader = await _context.CartHeader.FirstOrDefaultAsync(c => c.UserId == cartDto.CartHeader.UserId);
            getCartHeader.CouponCode = cartDto.CartHeader.CouponCode;
            _context.CartHeader.Update(getCartHeader);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CartUpSert(CartDto cartDto)
        {
            CartHeader getCartHeader = await _context.CartHeader.FirstOrDefaultAsync(c => c.UserId == cartDto.CartHeader.UserId);
            if (getCartHeader == null)
            {
                // if there was no cart found for that user then
                // create a new card header and
                var newCartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                _context.CartHeader.Add(newCartHeader);
                await _context.SaveChangesAsync();

                //  also cart details and assign a new id
                cartDto.CartDetails.First().CartHeaderId = newCartHeader.CartHeaderId;
                var cartDetails = _mapper.Map<CartDetail>(cartDto.CartDetails.First());
                _context.CartDetails.Add(cartDetails);
                await _context.SaveChangesAsync();
                return true;

            }
            else
            {
                // the cart was there and therefore we are
                // either adding a new item or updating the count of an item
                CartDetail getCartDetails = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == cartDto.CartDetails.First().ProductId
                && x.CartHeaderId == getCartHeader.CartHeaderId);
                if (getCartDetails == null)
                {
                    // it is a different product
                    cartDto.CartDetails.First().CartHeaderId = getCartHeader.CartHeaderId;
                    var cartDetails = _mapper.Map<CartDetail>(cartDto.CartDetails.First());
                    _context.CartDetails.Add(cartDetails);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // updating count
                    getCartDetails.Count += cartDto.CartDetails.First().Count;
                    _context.CartDetails.Update(getCartDetails);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            return false;
        }

        public async Task<CartDto> GetUserCart(Guid userId)
        {
            var cartHeader = await _context.CartHeader.FirstOrDefaultAsync(x => x.UserId == userId);
            var cartDetails = _context.CartDetails.Where(x => x.CartHeaderId == cartHeader.CartHeaderId);
            CartDto cart = new CartDto()
            {
                CartHeader = _mapper.Map<CartHeaderDto>(cartHeader),
                CartDetails = _mapper.Map<IEnumerable<CartDetailDto>>(cartDetails)
            };
            var products = await _productInterface.GetAllProductsAsync();
            foreach (var item in cart.CartDetails)
            {
                item.Product = products.FirstOrDefault(x => x.ProductId == item.ProductId);
                cart.CartHeader.OrderTotal += (int)(item.Count * item.Product.ProductPrice);
            }
            // coupon added
            if (!string.IsNullOrWhiteSpace(cart.CartHeader.CouponCode))
            {
                var coupon = await _couponInterface.GetCouponData(cart.CartHeader.CouponCode);
                if (coupon != null && cart.CartHeader.OrderTotal > coupon.CouponMinAmont)
                {
                    cart.CartHeader.OrderTotal -= coupon.CouponAmount;
                    cart.CartHeader.DiscountTotal = coupon.CouponAmount;
                }
            }
            return cart;
        }

        public async Task<bool> RemoveFromCart(Guid CartDetailId)
        {
            CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(x => x.CartDetailId == CartDetailId);
            // item count
            var itemCount = _context.CartDetails.Where(x => x.CartHeaderId == cartDetail.CartHeaderId).Count();
            _context.CartDetails.Remove(cartDetail);
            if (itemCount == 1)
            {
                _context.CartHeader.Remove(_context.CartHeader.FirstOrDefault(x => x.CartHeaderId == cartDetail.CartHeaderId));
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}