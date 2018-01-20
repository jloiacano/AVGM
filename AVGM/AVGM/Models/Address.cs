using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AVGM.Models
{
    public class Address
    {
        [Key]
        public Guid AddressID { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public virtual ICollection<Guardian> Guardian { get; set; }
        public virtual ICollection<Job> Job { get; set; }
    }
}