﻿using SomeShop.Exceptions;
using SomeShop.Models;
using SomeShop.Repositories;
using SomeShop.Services.Interfaces;
using System.Linq.Expressions;

namespace SomeShop.Services
{
	public class ProductService : IProductService
	{
		private readonly IGenericRepository<Product> _repository;

		public ProductService(IGenericRepository<Product> repository)
		{
			_repository = repository;
		}

		public async Task CreateProductAsync(Product item, CancellationToken cancellationToken)
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

		public async Task DeleteProductAsync(Product item, CancellationToken cancellationToken)
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

		public async Task DeleteProductByIdAsync(int id, CancellationToken cancellationToken)
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

		public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
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

		public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken)
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

		public async Task<IEnumerable<Product>> GetProductsAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken)
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

		public async Task UpdateProductAsync(Product item, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();

			try
			{
				await _repository.UpdateAsync(item, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				throw;
			}		
		}
	}
}
