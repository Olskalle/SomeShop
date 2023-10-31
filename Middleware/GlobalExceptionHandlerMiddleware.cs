using System.Net;

namespace SomeShop.Middleware
{
	public class GlobalExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, 
			ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
			_next = next;
			_logger = logger;
        }

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (OperationCanceledException)
			{
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, ex.Message);
				
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			}
		}
    }
}
