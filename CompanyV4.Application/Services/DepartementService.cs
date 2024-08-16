using CompanyV4.Domain;
using CompanyV4.Domain.Interfaces;
using CompanyV4.Domain.Models;
using CompanyV4.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyV4.Application.Services
{
    public class DepartementService : IDepartementService
    {
        private readonly CompanyContext _context;
        private readonly EmployeeOptions _employeeOptions;
        private readonly DepartmentOptions _departmentOptions;

        public DepartementService(CompanyContext context, IOptions<EmployeeOptions> employeeOptions, IOptions<DepartmentOptions> departmentOptions)
        {
            _context = context;
            _employeeOptions = employeeOptions.Value;
            _departmentOptions = departmentOptions.Value;
        }

        public async Task<Department> CreateDept(Department departement)
        {
            await _context.Departements.AddAsync(departement);
            await _context.SaveChangesAsync();
            return departement;
        }

        public IEnumerable<Department> GetAllDept()
        {
            return _context.Departements.ToList();
        }

        public Department GetDeptById(int id)
        {
            return _context.Departements.FirstOrDefault(d => d.DeptNo == id);
        }

        public string UpdateDept(int id, Department departement)
        {
            var existingDept = _context.Departements.FirstOrDefault(d => d.DeptNo == id);
            if (existingDept != null)
            {
                existingDept.DeptName = departement.DeptName;
                existingDept.MgrEmpNo = departement.MgrEmpNo;

                _context.Departements.Update(existingDept);
                _context.SaveChanges();
                return "Departement updated successfully";
            }
            return "Department tidak ditemukan";

        }

        public string DeleteDept(int id)
        {
            var departement = _context.Departements.FirstOrDefault(d => d.DeptNo == id);
            if (departement == null)
            {
                return "Departement not found";
            }

            _context.Departements.Remove(departement);
            _context.SaveChanges();
            return "Departement deleted successfully";
        }

        public IEnumerable<Employee> GetFemaleManagers()
        {
            var femaleManagers = _context.Departements
                .Where(d => d.MgrEmpNo.HasValue)
                .Select(d => d.Manager)
                .Where(e => e != null && e.Sex == "F")
                .OrderBy(e => e.LName)
                .ThenBy(e => e.FName)
                .ToList();

            return femaleManagers!;
        }

        public int CountFemaleManagers()
        {
            // Get count of female managers
            var count = _context.Departements
                .Where(d => d.MgrEmpNo.HasValue)
                .Select(d => d.Manager)
                .Count(e => e != null && e.Sex == "F");

            return count;
        }

        public IEnumerable<Employee> GetManagersRetire()
        {
            var tahunsekarang = DateTime.Now.Year;

            var managersRetire = _context.Departements
                .Where(d => d.MgrEmpNo.HasValue)
                .Select(d => d.Manager)
                .Where(e => e != null && e.DOB.HasValue && (tahunsekarang - e.DOB.Value.Year) == _employeeOptions.RetirementAge)
                .OrderBy(e => e.LName)
                .ToList();

            return managersRetire!;
        }

        public IEnumerable<Employee> GetManagersUnder40()
        {
            var tahunsekarang = DateTime.Now.Year;

            var managersUnder40 = _context.Departements
                .Where(d => d.MgrEmpNo.HasValue)
                .Select(d => d.Manager)
                .Where(e => e != null && e.DOB.HasValue && (tahunsekarang - e.DOB.Value.Year) < 40)
                .OrderBy(e => e.LName)
                .ToList();

            return managersUnder40!;
        }

    }
}
