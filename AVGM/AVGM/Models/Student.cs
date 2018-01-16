using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AVGM.Models
{
    public class Student
    {
        [Key]
        public Guid StudentID { get; set; }
        public string SSNumber { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public char Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Alergies { get; set; }
        public string MedicalConditions { get; set; }
        public string Medications { get; set; }
        public string MiscNotes { get; set; }
        public byte[] Photo { get; set; }

        public virtual ICollection<SchoolYear> Years { get; set; }
        public virtual ICollection<StudentGuardian> Guardians { get; set; }
    }
}