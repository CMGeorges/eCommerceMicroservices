using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Search.Interfaces
{
     public interface ISearchService
    {

        Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId);
    }
}
