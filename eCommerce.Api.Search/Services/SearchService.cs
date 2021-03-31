using eCommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductServices productServices;
        private readonly ICustomerService customerService;

        public SearchService(IOrdersService ordersService, IProductServices productServices, ICustomerService customerService)
        {
            this.ordersService = ordersService;
            this.productServices = productServices;
            this.customerService = customerService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productServices.GetProductsAsync();
            var customerResult = await customerService.GetCustomerByIdAsync(customerId);

            if (ordersResult.IsSuccess)
            {


                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSucess ?
                            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            "Product information is not available";                       

                    }            

                }


                var result = new
                {
                    Customer = customerResult.IsSuccess ? customerResult.Customer : new { Name = "Customer information is not available" },

                    Orders = ordersResult.Orders
                };
                return (true, result);

            }
            return (false, null);
        }
    }
}
