using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public static class OrderMapper
    {
        public static SalesOrder SerializeInvoiceToOrder(InvoiceModel invoice)
        {
            var salesOrderItems = invoice.LineItems.Select(item => new SalesOrderItem
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProduct(item.Product)
            }).ToList();

            return new SalesOrder
            {
                SalesOrderItems = salesOrderItems,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }

        public static List<OrderModel> SerializeOrdersToViewModel(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderModel
            {
                Id = order.Id,
                SalesOrderItems = SerializeSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomerModel(order.Customer),
                IsPaid = order.IsPaid,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn
            }).ToList();
        }

        private static List<SalesOrderItemModel> SerializeSalesOrderItems(List<SalesOrderItem> salesOrderItems)
        {
            return salesOrderItems.Select(item => new SalesOrderItemModel
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)
            }).ToList();
        }
    }
}