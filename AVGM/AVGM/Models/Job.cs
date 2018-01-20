using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AVGM.Models
{
    public class Job
    {
        [Key]
        public Guid JobID { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public virtual Address Address { get; set; }
    }
}