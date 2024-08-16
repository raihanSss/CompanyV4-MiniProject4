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
    public class EmployeeService : IEmployeeService
    {
        private readonly CompanyContext _context;
        private readonly EmployeeOptions _employeeOptions;
        private readonly DepartmentOptions _departmentOptions;

        public EmployeeService(CompanyContext context, IOptions<EmployeeOptions> employeeOptions, IOptions<DepartmentOptions> departmentOptions)
        {
            _context = context;
            _employeeOptions = employeeOptions.Value;
            _departmentOptions = departmentOptions.Value;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {

            // Validate MaxProjectsPerEmployee
            var projectCount = _context.Worksons.Count(wo => wo.EmpNo == employee.EmpNo);
            if (projectCount >= _employeeOptions.MaxProjectsPerEmployee)
            {
                throw new InvalidOperationException($"Employee has reached the maximum number of projects ({_employeeOptions.MaxProjectsPerEmployee}).");
            }


            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(emp => emp.EmpNo == id);
        }

        public string UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(emp => emp.EmpNo == id);
            if (existingEmployee != null)
            {
                // Jika DeptNo yang diberikan adalah 6 (departemen IT)
                if (employee.DeptNo == 6)
                {
                    // Hitung jumlah karyawan di departemen IT
                    var employeeCountInIT = _context.Employees.Count(e => e.DeptNo == 9);

                    // Validasi terhadap batas maksimum jumlah karyawan yang diperbolehkan
                    if (employeeCountInIT >= _departmentOptions.MaxEmployeesInIT && existingEmployee.DeptNo != 9)
                    {
                        return "The IT department has reached the maximum number of employees (6).";
                    }
                }

                // Lanjutkan update jika validasi lolos
                existingEmployee.FName = employee.FName;
                existingEmployee.LName = employee.LName;
                existingEmployee.Address = employee.Address;
                existingEmployee.DOB = employee.DOB;
                existingEmployee.Sex = employee.Sex;
                existingEmployee.Position = employee.Position;
                existingEmployee.DeptNo = employee.DeptNo;

                _context.Employees.Update(existingEmployee);
                _context.SaveChanges();
                return "Data employee berhasil diubah";
            }
            return "employee tidak ada";
        }

            public string DeleteEmployee(int id)
        {
            var employee = _context.Employees.FirstOrDefault(emp => emp.EmpNo == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return "Data employee berhasil dihapus";
            }
            return "Employee tidak ditemukan";
        }


        public IEnumerable<object> GetFemaleEmployeeBirthYearsAfter1990()
        {
            var result = _context.Employees
                .Where(e => e.Sex == "F" && e.DOB.HasValue && e.DOB.Value.Year > 1990)
                .Select(e => new
                {
                    e.FName,
                    BirthYear = e.DOB.Value.Year
                })
                .ToList();

            return result;
        }


        public IEnumerable<Employee> GetEmployeesFromBrics()
        {

            var bricsCountries = new[] { "Brazil", "Russia", "India", "China", "South Africa" };

            var employees = _context.Employees
                .Where(e => bricsCountries.Any(country => e.Address != null && e.Address.Contains(country)))
                .OrderBy(e => e.LName)
                .ToList();

            return employees;
        }

        public IEnumerable<Employee> GetEmployeesBornBetween1980And1990()
        {
            var employees = _context.Employees
                .Where(e => e.DOB.HasValue && e.DOB.Value.Year >= 1980 && e.DOB.Value.Year <= 1990)
                .ToList();

            return employees;
        }

        public IEnumerable<Employee> GetEmployeesNotManagers()
        {

            var managerIds = _context.Departements
                .Where(d => d.MgrEmpNo.HasValue)
                .Select(d => d.MgrEmpNo.Value)
                .ToList();


            var employees = _context.Employees
                .Where(e => !managerIds.Contains(e.EmpNo))
                .ToList();

            return employees;
        }

        public IEnumerable<Employee> GetEmployeesInITDepartment()
        {

            var employees = _context.Employees
                .Where(e => e.DeptNo == 6)
                .Select(e => new Employee
                {
                    FName = e.FName,
                    LName = e.LName,
                    Address = e.Address
                })
                .ToList();

            return employees;
        }

        public IEnumerable<object> GetEmployeesWithAge()
        {
            var currentYear = DateTime.Now.Year;


            var employeesWithAge = _context.Employees
                .Where(e => e.DOB.HasValue && e.DeptNo.HasValue)
                .Select(e => new
                {
                    Name = $"{e.FName} {e.LName}",
                    Department = e.Department.DeptName,
                    Age = currentYear - e.DOB.Value.Year
                })
                .ToList();

            return employeesWithAge;
        }
    }
}
