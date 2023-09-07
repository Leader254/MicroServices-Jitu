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
        public async Task<ActionResult<ResponseDto>> AddProduct(ProductsRequestDto products)
        {
            var newItem = _mapper.Map<Product>(products);
            var response = await _products.AddProductAsync(newItem);
            if (response != null)
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = response;
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
                _responseDto.Message = "";
                _responseDto.IsSuccess = true;
                _responseDto.Result = products;
                return Ok(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "";
            return BadRequest(_responseDto);
        }

        // get product by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetProductById(Guid id)
        {
            var products = await _products.GetProductByIdAync(id);
            if (products != null)
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "";
                _responseDto.Result = products;
                return Ok(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "";
            return BadRequest(_responseDto);
        }

        //update product
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(Guid id, ProductsRequestDto updateProduct)
        {
            var product = await _products.GetProductByIdAync(id);
            if (product == null)
            {
                _responseDto.Message = "Product Not Found";
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
            var updatedProduct = _mapper.Map(updateProduct, product);
            var newProduct = await _products.UpdateProductAsync(updatedProduct);
            if (newProduct != null)
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = newProduct;
                return Ok(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Failed";
            return BadRequest(_responseDto);
        }
        // delete product
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(Guid id)
        {
            var product = await _products.GetProductByIdAync(id);
            if (product == null)
            {
                _responseDto.Message = "Product Not Found";
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
            var deletedProduct = _products.DeleteProductAsync(product);
            if (deletedProduct != null)
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Success";
                return Ok(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Failed";
            return BadRequest(_responseDto);

        }
    }
}
