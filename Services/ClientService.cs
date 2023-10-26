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

		public ClientService(IGenericRepository<Client> repository)
		{
			_repository = repository;
		}

		public async Task CreateClientAsync(Client client, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.CreateAsync(client, cancellationToken);
		}

		public async Task DeleteClientAsync(Client client, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.RemoveAsync(client, cancellationToken);
		}

		public async Task DeleteClientByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.DeleteAsync(
				x => x.Id == id,
				cancellationToken);
		}

		public async Task<Client?> GetClientByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

			if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
		}

		public async Task<IEnumerable<Client>> GetClientsAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(cancellationToken);
		}

		public async Task<IEnumerable<Client>> GetClientsAsync(Expression<Func<Client, bool>> predicate,
			CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			return await _repository.GetAsync(predicate, cancellationToken);
		}

		public async Task UpdateClientAsync(Client client, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			await _repository.UpdateAsync(client, cancellationToken);
		}
	}
}
