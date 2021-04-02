using AutoMapper;
using eCommerce.Api.Orders.Db;
using eCommerce.Api.Orders.Profiles;
using eCommerce.Api.Orders.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace eCommerce.Api.Orders.Tests
{
    public class OrdersServicesTest
    {
        [Fact]
        public async Task GetOrdersReturnsAllOrdersByCustomerId()
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(nameof(GetOrdersReturnsAllOrdersByCustomerId))
                .Options;
            var dbContext = new OrderDbContext(options);
           

            //Mock Object for the mapper
            var orderProfile = new OrderProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(orderProfile));
            var mapper = new Mapper(configuration);

            var ordersProvider = new OrdersProvider(dbContext, null, mapper);

            var order = await ordersProvider.GetOrdersAsync(1);

            Assert.True(order.IsSuccess);
            Assert.True(order.Orders.Any());
            Assert.Null(order.ErrorMessage);

        }




        [Fact]
        public async Task GetOrdersReturnOrdersUsingInvalidCustomerId()
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(nameof(GetOrdersReturnOrdersUsingInvalidCustomerId))
                .Options;
            var dbContext = new OrderDbContext(options);
            

            //Mock Object for the mapper
            var orderProfile = new OrderProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(orderProfile));
            var mapper = new Mapper(configuration);

            var ordersProvider = new OrdersProvider(dbContext, null, mapper);

            var order = await ordersProvider.GetOrdersAsync(-1);

            Assert.False(order.IsSuccess);
            Assert.Null(order.Orders);

            Assert.NotNull(order.ErrorMessage);


        }



    }
}
