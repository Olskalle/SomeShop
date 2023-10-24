using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IClientService
    {
        // Manage Client
        Task CreateClientAsync(Client client, CancellationToken cancellationToken);
        Task<IEnumerable<Client>> GetClientsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Client>> GetClientsAsync(Expression<Func<Client, bool>> predicate, CancellationToken cancellationToken);
        Task<Client?> GetClientByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateClientAsync(Client client, CancellationToken cancellationToken);
        Task DeleteClientAsync(Client client, CancellationToken cancellationToken);
        Task DeleteClientByIdAsync(int id, CancellationToken cancellationToken);
    }
}
