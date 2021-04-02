using AutoMapper;
using eCommerce.Api.Customers.Db;
using eCommerce.Api.Customers.Profiles;
using eCommerce.Api.Customers.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace eCommerce.Api.Customers.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task GetCustomersReturnsAllCustomers()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(nameof(GetCustomersReturnsAllCustomers))
                .Options;
            var dbContext = new CustomerDbContext(options);
           

            //Mock Object for the mapper
            var customerProfile = new CustomerProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(customerProfile));
            var mapper = new Mapper(configuration);

            var customerProvider = new CustomerProvider(dbContext, null, mapper);

            var customer = await customerProvider.GetCustomersAsync();

            Assert.True(customer.IsSuccess);
            Assert.True(customer.customers.Any());
            Assert.Null(customer.ErrorMessage);

        }




        [Fact]
        public async Task GetCustomerReturnsCustomerUsingValidId()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(nameof(GetCustomerReturnsCustomerUsingValidId))
                .Options;
            var dbContext = new CustomerDbContext(options);
            //CreateProducts(dbContext);

            //Mock Object for the mapper
            var customerProfile = new CustomerProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(customerProfile));
            var mapper = new Mapper(configuration);

            var customersProvider = new CustomerProvider(dbContext, null, mapper);

            var customer = await customersProvider.GetCustomerAsync(1);

            Assert.True(customer.IsSuccess);
            Assert.NotNull(customer.Customer);
            Assert.True(customer.Customer.Id == 1);
            Assert.Null(customer.ErrorMessage);

        }


        [Fact]
        public async Task GetCustomerReturnsCustomerUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(nameof(GetCustomerReturnsCustomerUsingInvalidId))
                .Options;
            var dbContext = new CustomerDbContext(options);
            

            //Mock Object for the mapper
            var customerProfile = new CustomerProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(customerProfile));
            var mapper = new Mapper(configuration);

            var customersProvider = new CustomerProvider(dbContext, null, mapper);

            var customer = await customersProvider.GetCustomerAsync(-1);

            Assert.False(customer.IsSuccess);
            Assert.Null(customer.Customer);
            Assert.NotNull(customer.ErrorMessage);

        }
    }
}
