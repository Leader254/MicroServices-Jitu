using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Cart.Models;

namespace TheJitu_Commerce_Cart.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeader { get; set; }
    }
}