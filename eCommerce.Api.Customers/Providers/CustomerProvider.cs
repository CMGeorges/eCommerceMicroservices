using AutoMapper;
using eCommerce.Api.Customers.Db;
using eCommerce.Api.Customers.Interfaces;
using eCommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomersProvider
    {
        private readonly CustomerDbContext dbContext;
        private readonly ILogger<CustomerProvider> logger;
        private readonly IMapper mapper;

        public CustomerProvider(CustomerDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Data.Customer() { Id = 1, Name = "Flouflou", Address = "600 5th st Lunillville, Oc , USA" });
                dbContext.Customers.Add(new Db.Data.Customer() { Id = 2, Name = "David", Address = "5 boul Geeor st Lunillville, Oc , USA" });
                dbContext.Customers.Add(new Db.Data.Customer() { Id = 3, Name = "Soufiane", Address = "789 app 5 5th st Lunillville, Oc , USA" });
                dbContext.SaveChanges();
            }
        }


        /// <summary>
        /// Request from the DB  a Cutomer By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {

                logger?.LogInformation("Quering customer by Id");
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);

                if (customer != null )
                {
                    var result = mapper.Map<Db.Data.Customer, Models.Customer>(customer);
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
        /// Resquest all the customers 
        /// </summary>
        /// <returns></returns>
        public async Task<(bool IsSuccess, IEnumerable<Customer> customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Quering customers");
                var customers = await dbContext.Customers.ToListAsync();

                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Data.Customer>, IEnumerable<Models.Customer>>(customers);
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
