using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using tunenest.Domain.Exceptions;

namespace tunenest.Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<ValidationPipelineBehavior<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
        {
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var typeName = typeof(TRequest).Name;
            _logger.LogInformation("----- Validating command {CommandType}", typeName);

            if (!_validators.Any())
            {
                _logger.LogWarning("No validators found for command {CommandType}", typeName);
                return await next();
            }

            var validationFailures = new List<ValidationFailure>();

            foreach (var validator in _validators)
            {
                var validationResult =
                    await validator.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken);
                validationFailures.AddRange(validationResult.Errors);
            }

            if (validationFailures.Count == 0) return await next();

            _logger.LogError("Command validation failed: {CommandType}", typeName);

            var errors = validationFailures
                .Select(failure => new InvalidExceptionItemDto(failure.PropertyName, failure.ErrorMessage))
                .ToList();

            throw new InvalidValidationException(errors[0].code, errors[0].message, errors);
        }
    }
}
