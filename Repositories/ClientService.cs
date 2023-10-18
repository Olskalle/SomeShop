using SomeShop.Models;
using SomeShop.Services.Interfaces;

namespace SomeShop.Repositories
{
	public class ClientService : IClientService
	{
		private readonly IGenericRepository<Client> _repository;

        public ClientService(IGenericRepository<Client> repository)
        {
			_repository = repository;
        }

        public void CreateClient(Client client)
		{
			_repository.Create(client);
		}

		public void DeleteClient(Client client)
		{
			_repository.Remove(client);
		}

		public Client? GetClientById(int id)
		{
			var result = _repository.Get(x => x.Id == id);
			if (result.Count() > 1)
			{
				throw new InvalidOperationException($"Only one item expected, returned: {result.Count()}");
			}

			return result.FirstOrDefault();
		}

		public IEnumerable<Client> GetClients()
		{
			return _repository.Get();
		}

		public IEnumerable<Client> GetClients(Func<Client, bool> predicate)
		{
			return _repository.Get(predicate);
		}

		public void UpdateClient(Client client)
		{
			_repository.Update(client);
		}
	}
}
