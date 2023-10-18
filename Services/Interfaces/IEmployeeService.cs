using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
	public interface IEmployeeService
    {
        // Manage Employees
        void CreateEmployee(Employee item);
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployees(Func<Employee, bool> predicate);
        Employee? GetEmployeeById(int id);
        void UpdateEmployee(Employee item);
        void DeleteEmployee(Employee item);
    }
}
