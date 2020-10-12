using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public class ProductMapper
    {
        public static ProductModel SerializeProductModel(Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn
            };
        }

        public static Product SerializeProduct(ProductModel productModel)
        {
            return new Product
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                IsTaxable = productModel.IsTaxable,
                IsArchived = productModel.IsArchived,
                CreatedOn = productModel.CreatedOn,
                UpdatedOn = productModel.UpdatedOn
            };
        }
    }
}