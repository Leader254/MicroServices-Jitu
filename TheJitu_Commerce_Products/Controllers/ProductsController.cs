using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheJitu_Commerce_Products.Models;
using TheJitu_Commerce_Products.Models.Dtos;
using TheJitu_Commerce_Products.Services.IServices;

namespace TheJitu_Commerce_Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsInterface _products;
        private readonly IMapper _mapper;
        private readonly ResponseDto _responseDto;


        public ProductsController(IProductsInterface products, IMapper mapper)
        {
            _products = products;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }
        // create a product
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> AddProduct(ProductsRequestDto newProduct)
        {
            var newItem = _mapper.Map<Product>(newProduct);

            var response = await _products.AddProductAsync(newItem);
            if (response != null)
            {
                _responseDto.Message = response;
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }
            _responseDto.Message = "Bad Request";
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }
        // get all products
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetProducts()
        {
            var products = await _products.GetAllProductsAsync();
            if (products != null)
            {
                _responseDto.Message = " ";
                _responseDto.IsSuccess = true;
                _responseDto.Result = products;
                return Ok(_responseDto);
            }

            _responseDto.Message = " ";
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }

        // get product by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetProductsById(int id)
        {
            var products = await _products.GetProductByIdAync(id);
            if (products != null)
            {
                _responseDto.Message = "";
                _responseDto.IsSuccess = true;
                _responseDto.Result = products;
                return Ok(_responseDto);
            }

            _responseDto.Message = "Product Not Found ";
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }

        //update product
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(int id, ProductsRequestDto updateProduct)
        {
            var product = await _products.GetProductByIdAync(id);
            if (product == null)
            {
                _responseDto.Message = "Product Not Found";
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
            var updatedProduct = _mapper.Map(updateProduct, product);
            var isUpdated = await _products.UpdateProductAsync(updatedProduct);
            if (isUpdated != null)
            {
                _responseDto.Message = isUpdated;
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }

            _responseDto.Message = "Bad Request";
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }
        // delete product
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(int id)
        {
            var product = await _products.GetProductByIdAync(id);
            if (product == null)
            {
                _responseDto.Message = "Product Not Found";
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
            var isDeleted = await _products.DeleteProductAsync(product);
            if (isDeleted != null)
            {
                _responseDto.Message = isDeleted;
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }

            _responseDto.Message = "Bad Request";
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }
    }
}
