namespace SomeShop.Extensions
{
	public static class Extensions
	{
		public static async Task<T> RunWithCancellationHandling<T>(this Func<Task<T>> body, CancellationToken cancellationToken)
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
		public static async Task RunWithCancellationHandling(this Func<Task> body, CancellationToken cancellationToken)
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
		public static async Task RunWithCancellationHandling(this Task task, CancellationToken cancellationToken)
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
