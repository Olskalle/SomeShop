namespace SomeShop.Extensions
{
	public static class Extensions
	{
		private static async Task<T> WithCancellationHandling<T>(this CancellationToken cancellationToken, Func<Task<T>> body)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}

			try
			{
				return await body();
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}
		private static async Task WithCancellationHandling(this CancellationToken cancellationToken, Func<Task> body)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}

			try
			{
				await body();
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}
		private static async Task WithCancellationHandling(this CancellationToken cancellationToken, Task task)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException();
			}

			try
			{
				await task;
			}
			catch (OperationCanceledException)
			{
				throw;
			}
		}
	}
}
