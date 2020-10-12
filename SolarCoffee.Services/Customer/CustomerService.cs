using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDbContext _dbContext;

        public CustomerService(SolarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = customer,
                    Message = "New customer added",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    Data = null,
                    Message = e.StackTrace,
                    IsSuccess = false,
                    Time = DateTime.UtcNow
                };
            }
        }

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            var utcNow = DateTime.UtcNow;

            if (customer == null)
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Customer to delete not found",
                    IsSuccess = false,
                    Time = utcNow
                };

            try
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Customer to delete not found",
                    IsSuccess = true,
                    Time = utcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = e.StackTrace,
                    IsSuccess = false,
                    Time = utcNow
                };
            }
        }

        public List<Data.Models.Customer> GetAllCustomers()
        {
            return _dbContext.Customers.Include(c => c.PrimaryAddress)
                                       .OrderBy(c => c.LastName)
                                       .ToList();
        }

        public Data.Models.Customer GetCustomerById(int id)
        {
            return _dbContext.Customers.Find(id);
        }
    }
}