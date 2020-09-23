using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thinktecture.AKS.Sample.Configuration;
using Thinktecture.AKS.Sample.Models;
using Thinktecture.AKS.Sample.Services;

namespace Thinktecture.AKS.Sample.Controllers
{
    [Route("products")]
    public class ProductsController : Controller
    {
        public ProductsService ProductsService { get; }
        public BastaConfiguration BastaConfiguration { get; }

        public ProductsController(ProductsService productsService, BastaConfiguration config)
        {
            ProductsService = productsService;
            BastaConfiguration = config;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProductsAsync()
        {

            var allProducts = await ProductsService.GetAllProductsAsync();
            return Ok(allProducts.Take(BastaConfiguration.ListLimit));
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var product = await ProductsService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateProductAsync([FromBody] NewProductItem newProduct)
        {
            var product = await ProductsService.AddProductAsync(newProduct);
            return Created("", product);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, [FromBody] UpdateProductItem updateProduct)
        {
            var product = await ProductsService.UpdateProductAsync(id, updateProduct);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpDelete()]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            var deleted = await ProductsService.DeleteProductAsync(id);
            if (deleted)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
