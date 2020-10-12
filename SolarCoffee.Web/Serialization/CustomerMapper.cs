using System;
using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public class CustomerMapper
    {
        public static CustomerModel SerializeCustomerModel(Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress),
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn
            };
        }

        public static Customer SerializeCustomer(CustomerModel customer)
        {
            var address = new CustomerAddress
            {
                AddressLine = customer.PrimaryAddress.AddressLine,
                AddressLine2 = customer.PrimaryAddress.AddressLine2,
                City = customer.PrimaryAddress.City,
                State = customer.PrimaryAddress.State,
                Country = customer.PrimaryAddress.Country,
                PostalCode = customer.PrimaryAddress.PostalCode,
                CreatedOn = customer.PrimaryAddress.CreatedOn,
                UpdatedOn = customer.PrimaryAddress.UpdatedOn
            };

            return new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = address,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn
            };
        }
        public static CustomerAddressModel MapCustomerAddress(CustomerAddress address)
        {
            return new CustomerAddressModel
            {
                Id = address.Id,
                AddressLine = address.AddressLine,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                Country = address.Country,
                PostalCode = address.PostalCode,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
    }
}