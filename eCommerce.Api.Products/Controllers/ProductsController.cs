using eCommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Api.Products.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        #region Champs
        private readonly IProductsProvider productsProvider;

        #endregion

        #region Ctor
        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Return All the products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await productsProvider.GetProductsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.products);
            }

            return NotFound();
        }

        //TODO: Method that return a single product

        //TODO: Method that add a new product

        //TODO: MEthod that update a product that exist already

        //TODO: Method that Delete a product

        #endregion

    }
}
