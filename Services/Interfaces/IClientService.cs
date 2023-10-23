using SomeShop.Models;
using System.Linq.Expressions;

namespace SomeShop.Services.Interfaces
{
	public interface IClientService
    {
        // Manage Client
        void CreateClient(Client client);
        IEnumerable<Client> GetClients();
        IEnumerable<Client> GetClients(Expression<Func<Client, bool>> predicate);
        Client? GetClientById(int id);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
    }
}
