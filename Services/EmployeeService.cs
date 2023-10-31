using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class EmployeeService : IEmployeeService
	{
		private readonly IGenericRepository<Employee> _repository;
		private readonly ILogger<EmployeeService> _logger;

		public EmployeeService(IGenericRepository<Employee> repository, ILogger<EmployeeService> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task CreateEmployeeAsync(Employee item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("CREATE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
		}

		public async Task DeleteEmployeeAsync(Employee item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("DELETE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
		}

		public async Task DeleteEmployeeByIdAsync(int id, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("DELETE BY ID: {0}", id);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
				x => x.Id == id,
				cancellationToken);
		}

		public async Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET BY ID: {0}", id);
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Employee>> GetEmployeesAsync(CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET");
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(cancellationToken);
		}

		public async Task<IEnumerable<Employee>> GetEmployeesAsync(Expression<Func<Employee, bool>> predicate, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("GET WITH CONDITION");
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(predicate, cancellationToken);
		}

		public async Task UpdateEmployeeAsync(Employee item, CancellationToken cancellationToken)
		{
			_logger?.LogInformation("UPDATE: {0}", item);
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
		}
	}
}
