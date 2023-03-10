using FluentValidation;
using MediatR;

namespace Stix.Core.Pipeline
{
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : ResponseBase
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipeline(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var validationContext = new ValidationContext<TRequest>(request);

                var validationFailures = _validators
                    .Select(v => v.Validate(validationContext))
                    .Where(c => !c.IsValid)
                    .SelectMany(c => c.Errors)
                    .ToArray();

                if (validationFailures.Length > 0)
                {
                    throw new ValidationErrorException(validationFailures.ToValidationError());
                }
            }

            return await next();
        }
    }
}