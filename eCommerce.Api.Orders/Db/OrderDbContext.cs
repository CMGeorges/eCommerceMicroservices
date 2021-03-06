using eCommerce.Api.Orders.Db.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Orders.Db
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        public OrderDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
