using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _dbContext;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolarDbContext dbContext, ILogger<InventoryService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return _dbContext.ProductInventories.Include(pi => pi.Product)
                                                .Where(pi => !pi.Product.IsArchived)
                                                .ToList();
        }

        public ProductInventory GetProductInventoryByProductId(int productId)
        {
            return _dbContext.ProductInventories.Include(pi => pi.Product)
                                                .FirstOrDefault(pi => pi.Product.Id == productId);
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);

            return _dbContext.ProductInventorySnapshots.Include(pis => pis.Product)
                                                       .Where(pis => pis.SnapshotTime > earliest && !pis.Product.IsArchived)
                                                       .ToList();
        }

        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjusment)
        {
            try
            {
                var inventory = _dbContext.ProductInventories.Include(pi => pi.Product)
                                                             .First(pi => pi.Product.Id == id);

                inventory.QuantityOnHand += adjusment;

                try
                {
                    CreateSnapshot(inventory);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error creating inventory snapshot. StackTrace: {e.StackTrace}");
                }

                _dbContext.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    Data = inventory,
                    Message = $"Product {id} inventory adjusted",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<ProductInventory>
                {
                    Data = null,
                    Message = e.StackTrace,
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };
            }
        }

        private void CreateSnapshot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand,
                SnapshotTime = DateTime.UtcNow
            };

            _dbContext.Add(snapshot);
        }
    }
}