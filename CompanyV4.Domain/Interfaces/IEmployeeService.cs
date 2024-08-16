using CompanyV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyV4.Domain.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployeeAsync(Employee employee);
        IEnumerable<Employee> GetAllEmployee();
        Employee GetEmployeeById(int id);
        string UpdateEmployee(int id, Employee employee);
        string DeleteEmployee(int id);

        IEnumerable<object> GetFemaleEmployeeBirthYearsAfter1990();

        IEnumerable<Employee> GetEmployeesFromBrics();

        IEnumerable<Employee> GetEmployeesBornBetween1980And1990();

        IEnumerable<Employee> GetEmployeesNotManagers();

        IEnumerable<Employee> GetEmployeesInITDepartment();

        IEnumerable<object> GetEmployeesWithAge();
    }
}
