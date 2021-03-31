using eCommerce.Api.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Interfaces
{
    public interface IProductServices
    {
        Task<(bool IsSucess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}
