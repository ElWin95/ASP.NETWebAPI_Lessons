using Microsoft.EntityFrameworkCore;
using ShopAppP416.Models;

namespace ShopAppP416.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
