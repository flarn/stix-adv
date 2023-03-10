using MediatR;
using Stix.Core.Interfaces;

namespace Stix.Core.Pipeline
{
    public class AuditPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, IAudit
    {
        private readonly IRequestingUser _requestingUser;

        public AuditPipeline(IRequestingUser requestingUser)
        {
            _requestingUser = requestingUser;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var userName = _requestingUser.GetUserName();
            var requestName = request.GetType().Name;

            //log what the user did.?

            return await next();
        }
    }
}
