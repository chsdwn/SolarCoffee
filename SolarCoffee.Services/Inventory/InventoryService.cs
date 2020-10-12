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

        public void CreateSnapshot()
        {
            throw new System.NotImplementedException();
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return _dbContext.ProductInventories.Include(pi => pi.Product)
                                                .Where(pi => !pi.Product.IsArchived)
                                                .ToList();
        }

        public ProductInventory GetProductInventoryByProductId(int productId)
        {
            throw new System.NotImplementedException();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            throw new System.NotImplementedException();
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
                    CreateSnapshot();
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
    }
}