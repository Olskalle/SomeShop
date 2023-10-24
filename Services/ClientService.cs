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

			try
			{
				await _repository.CreateAsync(client, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
        }

		public async Task DeleteClientAsync(Client client, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.RemoveAsync(client, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<Client?>> GetClientByIdAsync(int id, CancellationToken cancellationToken)
        {
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				var result = await _repository.GetAsync(x => x.Id == id, cancellationToken);

				if (result is null) throw new NullReferenceException();

				if (result.Count() > 1) throw new KeyNotUniqueException();

				return result.FirstOrDefault();
			}
			catch (OperationCanceledException)
			{
				throw;
			}
        }

		public async Task<async Task<IEnumerable<Client>>> GetClientsAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				return await _repository.GetAsync(cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<async Task<IEnumerable<Client>>> GetClientsAsync(Expression<Func<Client, bool>> predicate, 
			CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				return await _repository.GetAsync(predicate, cancellationToken);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task UpdateClient(Client client, CancellationToken cancellationToken)
		{
			try
			{
				await _repository.UpdateAsync(client, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}
	}
}
