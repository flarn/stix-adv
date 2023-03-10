using Stix.Core.Models;

namespace Stix.Core
{
    public class ValidationErrorException : ApplicationException
    {
        public ValidationError ValidationError { get; }

        public ValidationErrorException(ValidationError validationError)
        {
            ValidationError = validationError;
        }
    }
}