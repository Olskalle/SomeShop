﻿using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class ShoppingSessionService : IShoppingSessionService
	{
		private readonly IGenericRepository<ShoppingSession> _repository;

		public ShoppingSessionService(IGenericRepository<ShoppingSession> repository)
		{
			_repository = repository;
		}

		public async Task CreateSessionAsync(ShoppingSession item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.CreateAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}	
		}

		public async Task DeleteSessionAsync(ShoppingSession item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.RemoveAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task DeleteSessionByIdAsync(int id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.DeleteAsync(
					x => x.Id == id,
					cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task<ShoppingSession?> GetSessionByIdAsync(int id, CancellationToken cancellationToken)
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

		public async Task<IEnumerable<ShoppingSession>> GetSessionsAsync(CancellationToken cancellationToken)
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

		public async Task<IEnumerable<ShoppingSession>> GetSessionsAsync(Expression<Func<ShoppingSession, bool>> predicate, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				return await _repository.GetAsync(predicate, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}

		public async Task UpdateSessionAsync(ShoppingSession session, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.UpdateAsync(session, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}		
		}
	}
}
