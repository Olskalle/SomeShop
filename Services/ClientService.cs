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

		public void CreateClient(Client client) => _repository.Create(client);

		public void DeleteClient(Client client) => _repository.Remove(client);

		public Client? GetClientById(int id)
        {
            var result = _repository.Get(x => x.Id == id);

            if (result is null) throw new NullReferenceException();

			if (result.Count() > 1) throw new KeyNotUniqueException();

			return result.FirstOrDefault();
        }

		public IEnumerable<Client> GetClients() => _repository.Get();

		public IEnumerable<Client> GetClients(Expression<Func<Client, bool>> predicate) => _repository.Get(predicate);
		public void UpdateClient(Client client) => _repository.Update(client);
	}
}
