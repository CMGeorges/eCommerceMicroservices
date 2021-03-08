using eCommerce.Api.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {

        Task<(bool IsSuccess, IEnumerable<Customer> customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id);

    }
}
