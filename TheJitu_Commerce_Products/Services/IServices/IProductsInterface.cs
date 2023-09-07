using TheJitu_Commerce_Products.Models;

namespace TheJitu_Commerce_Products.Services.IServices
{
    public interface IProductsInterface
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAync(Guid id);
        Task<string> AddProductAsync(Product product);
        Task<string> UpdateProductAsync(Product product);
        Task<string> DeleteProductAsync(Product product);
    }
}
