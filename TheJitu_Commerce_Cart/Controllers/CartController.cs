using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheJitu_Commerce_Cart.Models.Dtos;
using TheJitu_Commerce_Cart.Services.IService;

namespace TheJitu_Commerce_Cart.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartInterface _cartInterface;
        private readonly ResponseDto _responseDto;

        public CartController(ICartInterface cartInterface)
        {
            _cartInterface = cartInterface;
            _responseDto = new ResponseDto();
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CartUpSert(CartDto cartDto)
        {
            try
            {
                var response = await _cartInterface.CartUpSert(cartDto);
                if (response)
                {
                    _responseDto.IsSuccess = true;
                    _responseDto.Result = response;
                }
                else
                {
                    _responseDto.IsSuccess = false;
                    return BadRequest(_responseDto);
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpDelete]
        public async Task<ActionResult<ResponseDto>> RemoveFromCart([FromBody] Guid cartDetailsId)
        {
            try
            {
                var response = await _cartInterface.RemoveFromCart(cartDetailsId);
                _responseDto.Result = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDto>> ApplyCoupon(CartDto cartDto)
        {
            try
            {
                var response = await _cartInterface.ApplyCoupon(cartDto);
                _responseDto.Result = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetUserCart(Guid userId)
        {
            try
            {
                var response = await _cartInterface.GetUserCart(userId);
                _responseDto.Result = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

    }
}