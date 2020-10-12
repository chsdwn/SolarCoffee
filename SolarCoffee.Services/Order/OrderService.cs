using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _dbContext;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public OrderService(SolarDbContext dbContext,
            ILogger<OrderService> logger,
            IProductService productService,
            IInventoryService inventoryService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }

        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            _logger.LogInformation("Generating new order");

            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);
                var inventoryId = _inventoryService.GetProductInventoryByProductId(item.Product.Id).Id;
                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _dbContext.SalesOrders.Add(order);
                _dbContext.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Open order created",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = e.StackTrace,
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };
            }
        }

        public List<SalesOrder> GetOrders()
        {
            return _dbContext.SalesOrders.Include(so => so.Customer)
                                            .ThenInclude(c => c.PrimaryAddress)
                                         .Include(so => so.SalesOrderItems)
                                            .ThenInclude(soi => soi.Product)
                                         .ToList();
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var order = _dbContext.SalesOrders.Find(id);
            order.IsPaid = true;
            order.UpdatedOn = DateTime.UtcNow;

            try
            {
                _dbContext.SalesOrders.Update(order);
                _dbContext.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = $"Order {order.Id} closed: Invoice paid in full.",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = e.StackTrace,
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };
            }
        }
    }
}