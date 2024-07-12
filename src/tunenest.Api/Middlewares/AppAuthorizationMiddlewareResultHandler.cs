using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using tunenest.Domain.Exceptions;

namespace tunenest.Api.Middlewares
{
    public class AppAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly ILogger<AppAuthorizationMiddlewareResultHandler> _logger;
        private readonly AuthorizationMiddlewareResultHandler DefaultHandler = new();
        private const string forbiddenMessage = "Chưa cấp quyền để thực hiện yêu cầu này.";
        private const string unauthorizedMessage = "Chưa đăng nhập hoặc đã hết phiên làm việc.";

        public AppAuthorizationMiddlewareResultHandler(ILogger<AppAuthorizationMiddlewareResultHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden)
            {
                _logger.LogError(StatusCodes.Status403Forbidden, forbiddenMessage);
                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                var errors = new InvalidExceptionItemDto("Errors", forbiddenMessage);

                await context.Response.WriteAsJsonAsync(errors, default);
                return;
            }

            if (!authorizeResult.Succeeded && !context.User.Identity!.IsAuthenticated)
            {
                _logger.LogError(StatusCodes.Status401Unauthorized, unauthorizedMessage);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                var errors = new InvalidExceptionItemDto("Errors", unauthorizedMessage);

                await context.Response.WriteAsJsonAsync(errors, default);
                return;
            }

            await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
