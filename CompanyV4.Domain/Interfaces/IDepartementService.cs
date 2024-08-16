using CompanyV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyV4.Domain.Interfaces
{
    public interface IDepartementService
    {
        Task<Department> CreateDept(Department departement);
        IEnumerable<Department> GetAllDept();
        Department GetDeptById(int id);
        string UpdateDept(int id, Department departement);
        string DeleteDept(int id);

        IEnumerable<Employee> GetFemaleManagers();

        int CountFemaleManagers();

        IEnumerable<Employee> GetManagersRetire();

        IEnumerable<Employee> GetManagersUnder40();


    }
}
