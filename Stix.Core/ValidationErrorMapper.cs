using FluentValidation.Results;
using Stix.Core.Models;

namespace Stix.Core
{
    internal static class ValidationErrorMapper
    {
        public static ValidationError ToValidationError(this ValidationFailure[] validationFailures)
        {
            var validationError = new ValidationError(validationFailures.Length);

            Span<ValidationFailure> failuresSpan = validationFailures;

            for (int i = 0; i < failuresSpan.Length; i++)
            {

                var failure = failuresSpan[i];

                validationError.Add(i, failure.PropertyName, failure.ErrorMessage);
            }

            return validationError;

        }
    }
}
