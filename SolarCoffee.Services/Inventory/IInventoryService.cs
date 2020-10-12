using System.Collections.Generic;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        List<ProductInventory> GetCurrentInventory();
        ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjusment);
        ProductInventory GetProductInventoryByProductId(int productId);
        void CreateSnapshot();
        List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}