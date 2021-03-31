using AutoMapper;
using eCommerce.Api.Products.Db;
using eCommerce.Api.Products.Db.Data;
using eCommerce.Api.Products.Profiles;
using eCommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace eCommerce.Api.Products.Tests
{
    public class ProductsServicesTest
    {

        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProdutsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
           var dbContext = new ProdutsDbContext(options);
            CreateProducts(dbContext);

            //Mock Object for the mapper
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

           var productsProvider = new ProductsProvider(dbContext,null,mapper);

            var product = await productsProvider.GetProductsAsync();

            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Null(product.ErrorMessage);

        }




        [Fact]
        public async Task GetProductsReturnsProductsUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProdutsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductsUsingValidId))
                .Options;
            var dbContext = new ProdutsDbContext(options);
            //CreateProducts(dbContext);

            //Mock Object for the mapper
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(1);

            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.True(product.Product.Id == 1);
            Assert.Null(product.ErrorMessage);

        }


        [Fact]
        public async Task GetProductsReturnsProductsUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProdutsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductsUsingInvalidId))
                .Options;
            var dbContext = new ProdutsDbContext(options);
            //CreateProducts(dbContext);

            //Mock Object for the mapper
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(-1);

            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
           
            Assert.NotNull(product.ErrorMessage);

        }



        private void CreateProducts(ProdutsDbContext dbContext)
        {
            for (int i = 0; i <= 10; i++)
            {
                dbContext.Add( new Product()
                {
                    Id=i+4,
                    Name = Faker.Name.FullName(),
                    Inventory = Faker.Number.RandomNumber(15,150),
                    Price = (decimal)Faker.Number.Double()
                });
               
            }
            dbContext.SaveChanges();
        }
    }
}
