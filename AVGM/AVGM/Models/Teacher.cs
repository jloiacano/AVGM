using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AVGM.Models
{
    public class Teacher
    {
        [Key]
        public Guid TeacherID { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DOB { get; set; }
        public string Bio { get; set; }
        public byte[] Photo { get; set; }
    }
}