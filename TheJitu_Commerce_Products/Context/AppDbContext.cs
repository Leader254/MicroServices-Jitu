using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TheJitu_Commerce_Products.Models;

namespace TheJitu_Commerce_Products.Context
{

    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }

    }
}
