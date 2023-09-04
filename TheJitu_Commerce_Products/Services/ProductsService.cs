using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Products.Context;
using TheJitu_Commerce_Products.Models;
using TheJitu_Commerce_Products.Services.IServices;

namespace TheJitu_Commerce_Products.Services
{
    public class ProductsService : IProductsInterface
    {
        private readonly AppDbContext _context;

        public ProductsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return "Added Successfully";
        }

        public async Task<string> DeleteProductAsync(Product products)
        {
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return "Product Removed Successfully";

        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAync(int id)
        {
            return await _context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return "Product Updated Successfully";
        }
    }
}
