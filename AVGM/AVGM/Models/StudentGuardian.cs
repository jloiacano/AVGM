using System;
using System.ComponentModel.DataAnnotations;

namespace AVGM.Models
{
    public class StudentGuardian
    {
        public int ID { get; set; }
        public Guid StudentID { get; set; }
        public Guid GuardianID { get; set; }
        public virtual Student Student { get; set; }
        public virtual Guardian Guardian { get; set; }
    }
}