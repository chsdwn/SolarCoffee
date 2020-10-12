using System;

namespace SolarCoffee.Web.ViewModels
{
    public class CustomerAddressModel
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}