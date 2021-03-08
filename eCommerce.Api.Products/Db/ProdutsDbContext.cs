using eCommerce.Api.Products.Db.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Api.Products.Db
{
    public class ProdutsDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }

        public ProdutsDbContext(DbContextOptions options):base(options)
        {

        }

    }
}
