using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AVGM.Models
{
    public class GuardianAddress
    {
        [Key]
        public int GuardianAddressID { get; set; }
        public string TypeOfAddress { get; set; }
        public Guid GuardianID { get; set; }
        public Guid AddressID { get; set; }
        public virtual Guardian Guardian { get; set; }
        public virtual Address Address { get; set; }
    }
}