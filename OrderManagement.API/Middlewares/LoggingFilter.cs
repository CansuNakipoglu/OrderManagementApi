using Microsoft.AspNetCore.Mvc.Filters;

namespace OrderManagement.API.Middlewares
{
    public class LoggingFilter : IActionFilter
    {
        private readonly ILogger<LoggingFilter> _logger;

        public LoggingFilter(ILogger<LoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Action method is executing: {ActionName}", context.ActionDescriptor.DisplayName);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                _logger.LogInformation("Action method executed successfully: {ActionName}", context.ActionDescriptor.DisplayName);
            }
            else
            {
                _logger.LogError("Action method encountered an error: {ActionName}, Error: {Error}", context.ActionDescriptor.DisplayName, context.Exception.Message);
            }
        }
    }
}
