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

        public SearchService(IOrdersService ordersService, IProductServices productServices)
        {
            this.ordersService = ordersService;
            this.productServices = productServices;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productServices.GetProductsAsync();

            if (ordersResult.IsSuccess && productsResult.IsSucess)
            {

                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name;

                    }

                }


                var result = new
                {
                    Orders = ordersResult.Orders
                };
                return (true, result);

            }
            return (false, null);
        }
    }
}
