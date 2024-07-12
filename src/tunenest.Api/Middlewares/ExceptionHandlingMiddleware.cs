using tunenest.Domain.Exceptions;
using tunenest.Domain.Extensions;
using tunenest.Domain.Helpers;

namespace tunenest.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidValidationException ex)
            {
                _logger.LogError(ex, "FluentValidation failed");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.status;

                var errors = new InvalidExceptionItemDto(ex.code, ex.Message);

                var response = new CreateErrorResult<InvalidExceptionItemDto>(ex.status, "Validation failed", errors);

                var jsonResponse = response.ToJson();

                await context.Response.WriteAsync(jsonResponse);
            }
            catch (BusinessRuleException ex)
            {
                _logger.LogError(ex, "Business rule exception occurred");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex.status;

                var errors = new InvalidExceptionItemDto(ex.code, ex.Message);

                var response = new CreateErrorResult<InvalidExceptionItemDto>(ex.status, "Business rule failed", errors);

                var jsonResponse = response.ToJson();

                await context.Response.WriteAsync(jsonResponse);
            }
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "An unexpected error occurred");

            //    context.Response.ContentType = "application/json";
            //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            //    var errors = new InvalidExceptionItemDto(ex.Source, ex.Message);

            //    var response = await Result<InvalidExceptionItemDto>
            //        .ErrorAsync(context.Response.StatusCode, "An unexpected error occurred", errors);

            //    var jsonResponse = response.ToJson();

            //    await context.Response.WriteAsync(jsonResponse);
            //}
        }
    }
}
