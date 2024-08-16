using CompanyV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyV4.Domain.Interfaces
{
    public interface IProjectService
    {
        Task<Project> CreateProjAsync(Project project);
        IEnumerable<Project> GetAllProj();
        Project GetProjById(int id);
        string UpdateProj(Project project);
        string DeleteProj(int id);

        IEnumerable<Project> GetProjectsManagedByPlanningDept();
        IEnumerable<Project> GetProjectsWithNoEmployees();
        IEnumerable<object> GetDepartmentsWithMoreThanTenEmployees();
        IEnumerable<object> GetTotalHoursWorkedByFemaleEmployees();
        IEnumerable<Project> GetProjectsManagedByITAndHRDept();
        IEnumerable<object> GetNonManagersAndSupervisors();
        IEnumerable<object> GetFemaleManagersAndTheirProjects();
        IEnumerable<object> GetMaxMinHoursWorkedByEmployee();
        IEnumerable<object> GetTotalHoursWorkedForEachEmployee();
    }
}
