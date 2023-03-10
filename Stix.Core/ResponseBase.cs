using Stix.Core.Models;

namespace Stix.Core;

public class ResponseBase<T> : ResponseBase
{
    public ResponseBase(T? data)
    {
        Data = data;
    }

    public T? Data { get; }
}

public class ResponseBase
{
    public static ResponseBase ValidationFailure(ValidationError validationError) => new ResponseBase { ValidationError = validationError, };

    public static ResponseBase Failure(string message) => new ResponseBase { ErrorMessage = message };

    public ValidationError? ValidationError { get; private set; }
    public string? ErrorMessage { get; private set; }
}