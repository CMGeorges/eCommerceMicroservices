using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Orders.Profiles
{
    public class OrderProfile:AutoMapper.Profile
    {
        public OrderProfile()
        {

            CreateMap<Db.Data.Order, Models.Order>();
            CreateMap<Db.Data.OrderItem, Models.OrderItem>();

        }
    }
}
