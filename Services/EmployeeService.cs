using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IGenericRepository<Employee> _repository;

        public EmployeeService(IGenericRepository<Employee> repository)
        {
			_repository = repository;
        }

		public void CreateEmployee(Employee item) => _repository.Create(item);

		public void DeleteEmployee(Employee item) => _repository.Remove(item);

		public Employee? GetEmployeeById(int id)
		{
			var result = _repository.Get(x => x.Id == id);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public IEnumerable<Employee> GetEmployees() => _repository.Get();

		public IEnumerable<Employee> GetEmployees(Expression<Func<Employee, bool>> predicate) => _repository.Get(predicate);

		public void UpdateEmployee(Employee item) => _repository.Update(item);
	}
}
