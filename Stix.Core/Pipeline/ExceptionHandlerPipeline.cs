using MediatR;

namespace Stix.Core.Pipeline
{
    public class ExceptionHandlerPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : ResponseBase
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {

                return await next();
            }
            catch (Exception ex) when (ex is not EntityNotFoundException)
            {

                //log to insights etc

                // when ex is not EntityNotFoundException

                throw;
            }
        }
    }
}
