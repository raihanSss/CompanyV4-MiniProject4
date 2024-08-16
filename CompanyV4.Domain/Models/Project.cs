using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyV4.Domain.Models
{
    public class Project
    {
        [Key]
        public int ProjNo { get; set; }
        public string ProjName { get; set; }
        public int? DeptNo { get; set; }

        // Navigation properties
        public Department? Department { get; set; }
        public ICollection<WorksOn>? WorksOns { get; set; }
    }
}
