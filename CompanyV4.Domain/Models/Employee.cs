using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CompanyV4.Domain.Models
{
    public class Employee
    {
        [Key]
        public int EmpNo { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string? Address { get; set; }
        public DateOnly? DOB { get; set; }
        public string? Sex { get; set; }
        public string? Position { get; set; }
        public int? DeptNo { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Department? Department { get; set; }

        [JsonIgnore]
        public ICollection<WorksOn>? WorksOns { get; set; }
    }
}
