using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface async Task<IEmployeeService
    {
        // Manage Employees
        Task CreateEmployeeAsync(Employee item, CancellationToken cancellationToken);
        Task<async Task<IEnumerable<Employee>>> GetEmployeesAsync(CancellationToken cancellationToken);
        Task<async Task<IEnumerable<Employee>>> GetEmployeesAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken);
        Task<Employee?>> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateEmployeeAsync(Employee item, CancellationToken cancellationToken);
        Task DeleteEmployeeAsync(Employee item, CancellationToken cancellationToken);
    }
}
