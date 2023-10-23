using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IEmployeeService
    {
        // Manage Employees
        void CreateEmployee(Employee item);
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployees(Expression<Func<Employee, bool>> predicate);
        Employee? GetEmployeeById(int id);
        void UpdateEmployee(Employee item);
        void DeleteEmployee(Employee item);
    }
}
