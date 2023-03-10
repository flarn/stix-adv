using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;
using Stix.Core;
using Stix.Core.Interfaces;
using System.Linq.Expressions;

namespace Stix.CosmosDb
{
    public class CosmosDbRepository : IRepository
    {
        private readonly CosmosClient _client;
        private readonly CosmosDbSettings _options;

        public CosmosDbRepository(IOptions<CosmosDbSettings> options)
        {
            _options = options.Value;

            _client = new CosmosClient(_options.ConnectionString, new CosmosClientOptions { ConnectionMode = ConnectionMode.Direct });
        }

        public ItemRequestOptions CreateItemRequestOptions = new() { EnableContentResponseOnWrite = false };

        public async ValueTask<T?> GetById<T>(string id) where T : EntityBase
        {
            var container = GetDefaultContainer();
            try
            {
                return await container.ReadItemAsync<T>(id, new PartitionKey(id));
            }
            catch (CosmosException cosmosException) when (cosmosException.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default;
            }
        }

        public async ValueTask Create<T>(T entity) where T : EntityBase
        {
            Container container = GetDefaultContainer();

            await container.CreateItemAsync(entity, new PartitionKey(entity.id), CreateItemRequestOptions);
        }

        public async ValueTask Replace<T>(T entity) where T : EntityBase
        {
            Container container = GetDefaultContainer();

            await container.ReplaceItemAsync(entity, entity.id, new PartitionKey(entity.id));
        }

        public async ValueTask Delete<T>(string id) where T : EntityBase
        {
            Container container = GetDefaultContainer();

            await container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }

        public async ValueTask<List<T>> QueryAsync<T>(Expression<Func<T, bool>> filterPredicate, int skip, int take, Expression<Func<T, object>> orderBy) where T : EntityBase
        {
            Container container = GetDefaultContainer();

            var query = container
                .GetItemLinqQueryable<T>()
                .Where(filterPredicate)
                .OrderByDescending(orderBy)
                .Skip(skip)
                .Take(take);

            using FeedIterator<T> iterator = query.ToFeedIterator();

            var results = new List<T>(take);

            while (iterator.HasMoreResults)
            {
                foreach (var item in await iterator.ReadNextAsync())
                {
                    results.Add(item);
                }
            }

            return results;
        }


        private Container GetDefaultContainer()
        {
            return _client.GetDatabase(_options.DatabaseName).GetContainer(_options.ContainerName);
        }
    }
}