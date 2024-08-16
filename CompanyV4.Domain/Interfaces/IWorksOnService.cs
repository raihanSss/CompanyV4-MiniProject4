using CompanyV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyV4.Domain.Interfaces
{
    public interface IWorksOnService
    {
        IEnumerable<WorksOn> GetAllWorksOns();
        WorksOn GetWorksOnById(int id);
        Task<string> UpdateHoursWorkedAsync(int worksOnId, int hoursWorked);
    }
}
