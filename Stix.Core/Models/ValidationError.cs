namespace Stix.Core.Models
{
    public class ValidationError
    {
        public ValidationError(int failureCount)
        {

            Errors = new ValidationErrorMessage[failureCount];
        }

        public ValidationErrorMessage[] Errors { get; }

        internal void Add(int index, string propertyName, string errorMessage)
        {
            Errors[index] = new ValidationErrorMessage(propertyName, errorMessage);
        }
    }
    public class ValidationErrorMessage
    {
        public ValidationErrorMessage(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public string PropertyName { get; }
        public string ErrorMessage { get; }
    }
}
