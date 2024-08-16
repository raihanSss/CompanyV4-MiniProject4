using CompanyV4.Domain;
using CompanyV4.Domain.Interfaces;
using CompanyV4.Domain.Models;
using CompanyV4.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyV4.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly CompanyContext _context;
        private readonly ProjectOptions _projectOptions;
        private readonly EmployeeOptions _employeeOptions;


        public ProjectService(CompanyContext context, IOptions<ProjectOptions> projectOptions, IOptions<EmployeeOptions> employeeOptions)
        {
            _context = context;
            _projectOptions = projectOptions.Value;
            _employeeOptions = employeeOptions.Value;
        }

        public async Task<Project> CreateProjAsync(Project project)
        {
            // MaxProjectsPerDepartment
            var projectCount = _context.Projects.Count(p => p.DeptNo == project.DeptNo);
            if (projectCount >= _projectOptions.MaxProjectsPerDepartment)
            {
                throw new InvalidOperationException($"Department has reached the maximum number of projects ({_projectOptions.MaxProjectsPerDepartment}).");
            }

            // MaxWorkingHours
            var totalHours = project.WorksOns?.Sum(wo => wo.HoursWorked) ?? 0;
            if (totalHours > _projectOptions.MaxWorkingHours)
            {
                throw new InvalidOperationException($"The project exceeds the maximum allowed working hours ({_projectOptions.MaxWorkingHours}).");
            }

            if (project.WorksOns != null)
            {
                foreach (var worksOn in project.WorksOns)
                {
                    var employeeProjectCount = _context.Worksons.Count(wo => wo.EmpNo == worksOn.EmpNo);

                    if (employeeProjectCount >= _employeeOptions.MaxProjectsPerEmployee)
                    {
                        throw new InvalidOperationException($"Employee {worksOn.EmpNo} has reached the maximum number of projects ({_employeeOptions.MaxProjectsPerEmployee}).");
                    }
                }
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return project;
        }


        public IEnumerable<Project> GetAllProj()
        {
            return _context.Projects
                .Include(p => p.WorksOns)
                .ThenInclude(wo => wo.Employee)
                .ToList();
        }

        public Project GetProjById(int id)
        {
            return _context.Projects
                .Include(p => p.WorksOns)
                .ThenInclude(wo => wo.Employee)
                .FirstOrDefault(p => p.ProjNo == id);
        }

        public string UpdateProj(Project project)
        {
            var existingProject = _context.Projects.Include(p => p.WorksOns).FirstOrDefault(p => p.ProjNo == project.ProjNo);
            if (existingProject == null)
            {
                return "Project not found";
            }

            existingProject.ProjName = project.ProjName;
            existingProject.DeptNo = project.DeptNo;

            // Update the employees working on the project
            if (project.WorksOns != null && project.WorksOns.Any())
            {
                // Clear existing employees
                _context.Worksons.RemoveRange(existingProject.WorksOns);

                // Add new employees
                foreach (var worksOn in project.WorksOns)
                {
                    _context.Worksons.Add(worksOn);
                }
            }

            _context.SaveChanges();
            return "Project updated successfully";
        }

        public string DeleteProj(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return "Project not found";
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();
            return "Project deleted successfully";
        }

        public IEnumerable<Project> GetProjectsManagedByPlanningDept()
        {
            return from p in _context.Projects
                   join d in _context.Departements on p.DeptNo equals d.DeptNo
                   where d.DeptName == "Planning"
                   select p;
        }

        public IEnumerable<Project> GetProjectsWithNoEmployees()
        {
            return from p in _context.Projects
                   where !_context.Worksons.Any(wo => wo.ProjNo == p.ProjNo)
                   select p;
        }

        public IEnumerable<object> GetDepartmentsWithMoreThanTenEmployees()
        {
            return from e in _context.Employees
                   group e by e.DeptNo into deptGroup
                   where deptGroup.Count() > 10
                   select new
                   {
                       DeptNo = deptGroup.Key,
                       EmployeeCount = deptGroup.Count()
                   };
        }

        public IEnumerable<object> GetTotalHoursWorkedByFemaleEmployees()
        {
            return from wo in _context.Worksons
                   join e in _context.Employees on wo.EmpNo equals e.EmpNo
                   where e.Sex == "F"
                   group wo by new { e.DeptNo, e.LName } into employeeGroup
                   orderby employeeGroup.Key.DeptNo, employeeGroup.Key.LName
                   select new
                   {
                       DeptNo = employeeGroup.Key.DeptNo,
                       LastName = employeeGroup.Key.LName,
                       TotalHoursWorked = employeeGroup.Sum(wo => wo.HoursWorked)
                   };
        }

        public IEnumerable<Project> GetProjectsManagedByITAndHRDept()
        {
            return from p in _context.Projects
                   join d in _context.Departements on p.DeptNo equals d.DeptNo
                   where d.DeptName == "IT" || d.DeptName == "HR"
                   select p;
        }

        public IEnumerable<object> GetNonManagersAndSupervisors()
        {
            return from e in _context.Employees
                   where e.Position != "Manager" && e.Position != "Supervisor"
                   select new
                   {
                       e.FName,
                       e.LName,
                       e.Position,
                       e.Sex,
                       e.DeptNo
                   };
        }

        public IEnumerable<object> GetFemaleManagersAndTheirProjects()
        {
            return from e in _context.Employees
                   join d in _context.Departements on e.EmpNo equals d.MgrEmpNo
                   join p in _context.Projects on d.DeptNo equals p.DeptNo
                   where e.Sex == "F" && e.Position == "Manager"
                   select new
                   {
                       ManagerName = e.FName + " " + e.LName,
                       p.ProjName
                   };
        }

        public IEnumerable<object> GetMaxMinHoursWorkedByEmployee()
        {
            return from wo in _context.Worksons
                   group wo by wo.EmpNo into employeeGroup
                   select new
                   {
                       EmpNo = employeeGroup.Key,
                       MaxHoursWorked = employeeGroup.Max(wo => wo.HoursWorked),
                       MinHoursWorked = employeeGroup.Min(wo => wo.HoursWorked)
                   };
        }

        public IEnumerable<object> GetTotalHoursWorkedForEachEmployee()
        {
            return from wo in _context.Worksons
                   group wo by wo.EmpNo into employeeGroup
                   select new
                   {
                       EmpNo = employeeGroup.Key,
                       TotalHoursWorked = employeeGroup.Sum(wo => wo.HoursWorked)
                   };
        }
    }
}
