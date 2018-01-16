using System.ComponentModel.DataAnnotations;

namespace AVGM.Models
{
    public class SchoolYear
    {
        [Key]
        public string Year { get; set; }
    }
}