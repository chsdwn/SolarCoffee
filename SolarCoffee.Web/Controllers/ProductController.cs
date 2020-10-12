using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serialization;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts();
            var productViewModels = products.Select(p => ProductMapper.SerializeProductModel(p));
            return Ok(productViewModels);
        }

        [HttpPatch("{id}")]
        public IActionResult ArchiveProduct(int id)
        {
            _logger.LogInformation("Archiving product");
            var response = _productService.ArchiveProduct(id);
            return Ok(response);
        }
    }
}