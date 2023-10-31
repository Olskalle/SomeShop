using Microsoft.AspNetCore.Http.Features;

namespace SomeShop.Middleware
{
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<LoggingMiddleware>? _logger;

		public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware>? logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var method = context.Request.Method;
			var path = context.Request.Path.Value;
			var identifier = context.TraceIdentifier;
			_logger?.LogInformation($"[{method}]: {path}, Identifier: {identifier}");

			await _next(context);

			var response = context.Response.StatusCode.ToString();
			_logger?.LogInformation($"Response: {response}, Identifier: {identifier}");
		}
	}
}
