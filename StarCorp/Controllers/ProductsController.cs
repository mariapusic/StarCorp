using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarCorp.Data;
using StarCorp.Models;
using System;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using StarCorp.Abstractions;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace StarCorp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductDataService _productDataService;

        public ProductsController(ILogger<ProductsController> logger, IProductDataService productDataService)
        {
            _logger = logger;
            _productDataService = productDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int count = 10, int page = 1 )
        {
            return Ok((await _productDataService.GetProductsAsync()).Skip(count * page -1).Take(count));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var updateProduct = await _productDataService.UpdateProductAsync(product);

            return Ok(updateProduct);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid productId, Product product)
        {
            await _productDataService.DeleteProductAsync(product);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            IProduct creatingProduct = await _productDataService.CreateProductAsync(product);

            return Ok(creatingProduct);
        }
    }
}