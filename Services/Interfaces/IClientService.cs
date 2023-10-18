using SomeShop.Models;

namespace SomeShop.Services.Interfaces
{
	public interface IClientService
    {
        // Manage Client
        void CreateClient(Client client);
        IEnumerable<Client> GetClients();
        IEnumerable<Client> GetClients(Func<Client, bool> predicate);
        Client? GetClientById(int id);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
    }
}
