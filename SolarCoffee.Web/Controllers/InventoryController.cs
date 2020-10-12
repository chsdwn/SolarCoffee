using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _inventoryService;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public IActionResult GetCurrentInventory()
        {
            _logger.LogInformation("Getting all inventory");
            var inventory = _inventoryService.GetCurrentInventory()
                .Select(pi => new ProductInventoryModel
                {
                    Id = pi.Id,
                    QuantityOnHand = pi.QuantityOnHand,
                    IdealQuantity = pi.IdealQuantity,
                    Product = ProductMapper.SerializeProductModel(pi.Product)
                })
                .OrderBy(pi => pi.Product.Name)
                .ToList();
            return Ok(inventory);
        }

        [HttpPatch]
        public IActionResult UpdateInventory([FromBody] ShipmentModel shipment)
        {
            var id = shipment.ProductId;
            var adjusment = shipment.Adjusment;
            _logger.LogInformation($"Updating inventory for {id}, adjusment: {adjusment}");

            var inventory = _inventoryService.UpdateUnitsAvailable(id, adjusment);
            return Ok(inventory);
        }
    }
}