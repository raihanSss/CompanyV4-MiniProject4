using CompanyV4.Domain.Interfaces;
using CompanyV4.Domain.Models;
using CompanyV4.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyV4.Application.Services
{
    public class WorksService : IWorksOnService
    {
        private readonly CompanyContext _context;

        public WorksService(CompanyContext context)
        {
            _context = context;
        }

        public IEnumerable<WorksOn> GetAllWorksOns()
        {
            return _context.Worksons
                .Select(wo => new WorksOn
                {
                    Id = wo.Id,
                    ProjNo = wo.ProjNo,
                    EmpNo = wo.EmpNo,
                    DateWorked = wo.DateWorked,
                    HoursWorked = wo.HoursWorked,
                    Project = wo.Project != null ? new Project
                    {
                        ProjNo = wo.Project.ProjNo,
                        ProjName = wo.Project.ProjName,
                        DeptNo = wo.Project.DeptNo
                    } : null,
                    Employee = wo.Employee != null ? new Employee
                    {
                        EmpNo = wo.Employee.EmpNo,
                        FName = wo.Employee.FName,
                        LName = wo.Employee.LName
                    } : null
                })
                .ToList();
        }

        public WorksOn GetWorksOnById(int id)
        {
            return _context.Worksons
                .Where(wo => wo.Id == id)
                .Select(wo => new WorksOn
                {
                    Id = wo.Id,
                    ProjNo = wo.ProjNo,
                    EmpNo = wo.EmpNo,
                    DateWorked = wo.DateWorked,
                    HoursWorked = wo.HoursWorked,
                    Project = wo.Project != null ? new Project
                    {
                        ProjNo = wo.Project.ProjNo,
                        ProjName = wo.Project.ProjName,
                        DeptNo = wo.Project.DeptNo
                    } : null,
                    Employee = wo.Employee != null ? new Employee
                    {
                        EmpNo = wo.Employee.EmpNo,
                        FName = wo.Employee.FName,
                        LName = wo.Employee.LName
                    } : null
                })
                .FirstOrDefault();
        }

        public async Task<string> UpdateHoursWorkedAsync(int worksOnId, int hoursWorked)
        {
            var worksOn = await _context.Worksons.FindAsync(worksOnId);
            if (worksOn == null)
            {
                return "WorksOn entry not found.";
            }



            worksOn.HoursWorked = hoursWorked;
            await _context.SaveChangesAsync();

            return "Hours worked updated successfully.";
        }
    }

}
