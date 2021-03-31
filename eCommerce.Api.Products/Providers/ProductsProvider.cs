using AutoMapper;
using eCommerce.Api.Products.Db;
using eCommerce.Api.Products.Interfaces;
using eCommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProdutsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProdutsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private  void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Data.Product() {Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Db.Data.Product() {Id = 2, Name = "Mouse", Price = 5, Inventory = 300 });
                dbContext.Products.Add(new Db.Data.Product() {Id = 3, Name = "Monitor", Price = 150, Inventory = 254 });
                dbContext.Products.Add(new Db.Data.Product() {Id = 4, Name = "CPU", Price = 200, Inventory = 156 });
                dbContext.SaveChanges();
            }
        }


        /// <summary>
        /// Request all the products from the DB. if true it return the product. on the false it return not found or if 
        /// a error is catched return the error.
        /// </summary>
        /// <returns>Async</returns>
        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();

                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Data.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }


        /// <summary>
        /// Request a product by ID. On true return the product. On False return 404 or a error.
        /// </summary>
        /// <param name="id">Async</param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product != null)
                {
                    var result = mapper.Map<Db.Data.Product, Models.Product>(product);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {

                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
