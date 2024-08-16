using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace CompanyV4.Domain.Models
{
    public class Department
    {
        [Key]
        public int DeptNo { get; set; }
        public string DeptName { get; set; }
        public int? MgrEmpNo { get; set; }

        [JsonIgnore]
        public Employee? Manager { get; set; }
        [JsonIgnore]
        public ICollection<Employee>? Employees { get; set; }
        [JsonIgnore]
        public ICollection<Project>? Projects { get; set; }
    }
}
