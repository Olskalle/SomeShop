using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SomeShop
{
	public class LogActionAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext context)
		{
			var logger = context.Request.GetDependencyScope().GetService(typeof(ILogger<LogActionAttribute>)) as ILogger<LogActionAttribute>;
			logger?.LogInformation("Request: {0}", context.RequestContext.RouteData.Route.ToString());
			base.OnActionExecuting(context);
		}
	}
}
