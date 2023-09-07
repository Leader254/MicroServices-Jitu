using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TheJitu_Commerce_Cart.Models.Dtos;
using TheJitu_Commerce_Cart.Services.IService;
using Newtonsoft.Json;

namespace TheJitu_Commerce_Cart.Services
{
    public class ProductService : IProductInterface
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            // create a client
            var client = _clientFactory.CreateClient("Product");
            var response = await client.GetAsync("/api/Products");
            var content = await response.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(Convert.ToString(responseDto.Result));
            }
            return new List<ProductDto>();
        }
    }
}