using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Customers.Models
{
    public class Customer
    {
        #region Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }


        #endregion
    }
}
