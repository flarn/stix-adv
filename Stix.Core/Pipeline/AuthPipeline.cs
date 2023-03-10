using MediatR;
using Stix.Core.Interfaces;
using System.Reflection;
using Stix.Core.Models;

namespace Stix.Core.Pipeline
{
    //fördel med detta är att plocka bort validering av roller etc from apiet..
    public class AuthPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Type _authAttributeType = typeof(CustomAuthAttribute);
        private readonly IAuthAccessor _authAccessor;
        public AuthPipeline(IAuthAccessor authAccessor) => _authAccessor = authAccessor;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authAttribute = request.GetType().CustomAttributes
                .FirstOrDefault(a => a.AttributeType == _authAttributeType);

            if (authAttribute != null)
            {
                var authContext = _authAccessor.GetAuthContext();
                if (!IsAuthorized(authAttribute, authContext))
                    throw new UnauthorizedAccessException(); //?;
            }

            return await next();
        }

        private bool IsAuthorized(CustomAttributeData authAttribute, Auth? authContext)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomAuthAttribute : Attribute
    {


    }
}
