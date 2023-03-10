using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stix.Core.Interfaces;

namespace Stix.CosmosDb
{
    public static class Configure
    {
        public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CosmosDbSettings>(options => configuration.GetSection("CosmosDb").Bind(options));
            services.AddSingleton<IRepository, CosmosDbRepository>();

            return services;
        }
    }
}
