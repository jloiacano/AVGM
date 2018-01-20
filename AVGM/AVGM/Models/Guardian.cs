using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AVGM.Models
{
    public class Guardian
    {
        [Key]
        public Guid GuardianID { get; set; }
        public string FName { get; set; }
        public string DisplayName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public char Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string Relationship { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool LivesWithStudent { get; set; }
        public bool SharingContactInfo { get; set; }
        public byte[] Photo { get; set; }

        public virtual ICollection<StudentGuardian> Students { get; set; }
        
        public virtual Address Address { get; set; }
        public virtual Job Job { get; set; }
    }
}