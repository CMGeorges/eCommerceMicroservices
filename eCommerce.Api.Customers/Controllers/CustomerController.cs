using eCommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Customers.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomerController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }



        /// <summary>
        /// Get All Customer
        /// </summary>
        /// <returns>Async</returns>
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.customers);
            }

            return NotFound();
        }


        /// <summary>
        /// Get Customer by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Async</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound();
        }

    }
}
