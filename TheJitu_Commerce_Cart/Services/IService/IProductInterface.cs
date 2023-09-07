using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheJitu_Commerce_Cart.Models.Dtos;

namespace TheJitu_Commerce_Cart.Services.IService
{
    public interface IProductInterface
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}