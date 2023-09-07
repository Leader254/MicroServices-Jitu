using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_EmailService.Models;

namespace TheJitu_Commerce_EmailService.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<EmailLoggers> EmailLoggers { get; set; }
    }
}
