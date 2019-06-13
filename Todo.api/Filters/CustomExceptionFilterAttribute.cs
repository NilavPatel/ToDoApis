using Microsoft.AspNetCore.Mvc.Filters;
using Todo.api.Logger;

namespace Todo.api.Filters
{
    public class CustomExceptionFilterAttribute: ExceptionFilterAttribute
    {
        private readonly ICustomLogger _logger;

        public CustomExceptionFilterAttribute()
        {
            _logger = CustomLoggerFactory.GetLogger();
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogException(context.Exception);            
        }
    }
}
