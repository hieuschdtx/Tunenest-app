using MediatR;
using Microsoft.Extensions.Logging;

namespace tunenest.Application.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;
        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {RequestName} with payload: {@Request}", typeof(TRequest).Name, request);

            // Call the next delegate/middleware in the pipeline
            var response = await next();

            // Log response
            _logger.LogInformation("Handled {RequestName} with response: {@Response}", typeof(TRequest).Name, response);

            return response;
        }
    }
}
