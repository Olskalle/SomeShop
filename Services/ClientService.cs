using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class ClientService : IClientService
	{
		private readonly IGenericRepository<Client> _repository;
		private readonly ILogger<ClientService>? _logger;

		public ClientService(IGenericRepository<Client> repository, ILogger<ClientService>? logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task CreateClientAsync(Client item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(item, cancellationToken);
			_logger?.LogInformation("CREATE: {0}", item);
		}

		public async Task DeleteClientAsync(Client item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(item, cancellationToken);
			_logger?.LogInformation("DELETE: {0}", item);
		}

		public async Task DeleteClientByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
				x => x.Id == id,
				cancellationToken);
			_logger?.LogInformation("DELETE BY ID: {0}", id);
		}

		public async Task<Client?> GetClientByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			_logger?.LogInformation("GET BY ID: {0}", id);
			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Client>> GetClientsAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetWithIncludeAsync(cancellationToken, 
				client => client.Sessions, 
				client => client.Orders);
			_logger?.LogInformation("GET");
			return result;
		}

		public async Task<IEnumerable<Client>> GetClientsAsync(Expression<Func<Client, bool>> predicate,
			CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(predicate, cancellationToken);
			_logger?.LogInformation("GET WITH CONDITION");
			return result;
		}

		public async Task UpdateClientAsync(Client item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(item, cancellationToken);
			_logger?.LogInformation("UPDATE: {0}", item);
		}

	}
}
