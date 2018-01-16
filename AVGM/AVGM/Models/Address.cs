using System;
using System.ComponentModel.DataAnnotations;

namespace AVGM.Models
{
    public class Address
    {
        public Guid AddressID { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddressSuffix { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}