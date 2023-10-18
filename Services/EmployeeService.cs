using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;

namespace SomeShop.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IGenericRepository<Employee> _repository;

        public EmployeeService(IGenericRepository<Employee> repository)
        {
			_repository = repository;
        }

        public void CreateEmployee(Employee item)
		{
			_repository.Create(item);
		}

		public void DeleteEmployee(Employee item)
		{
			_repository.Remove(item);
		}

		public Employee? GetEmployeeById(int id)
		{
			var result = _repository.Get(x => x.Id == id);
			if (result.Count() > 1)
			{
				throw new InvalidOperationException($"Only one item expected, returned: {result.Count()}");
			}

			return result.FirstOrDefault();
		}

		public IEnumerable<Employee> GetEmployees()
		{
			return _repository.Get();
		}

		public IEnumerable<Employee> GetEmployees(Func<Employee, bool> predicate)
		{
			return _repository.Get(predicate);
		}

		public void UpdateEmployee(Employee item)
		{
			_repository.Update(item);
		}
	}
}
