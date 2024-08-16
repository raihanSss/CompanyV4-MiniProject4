using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CompanyV4.Domain.Models
{
    public class WorksOn
    {
        [Key]
        public int Id { get; set; }
        public int? EmpNo { get; set; }
        public int? ProjNo { get; set; }
        public DateTime? DateWorked { get; set; }
        public int? HoursWorked { get; set; }

        // Composite key can be set in DbContext configuration
        // Navigation properties
        [JsonIgnore]
        public Employee? Employee { get; set; }
        [JsonIgnore]
        public Project? Project { get; set; }
    }
}
