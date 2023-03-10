using System.Linq.Expressions;

namespace Stix.Core.Interfaces;

public interface IRepository
{
    ValueTask Create<T>(T entity) where T : EntityBase;
    ValueTask Delete<T>(string id) where T : EntityBase;
    ValueTask<T?> GetById<T>(string id) where T : EntityBase;
    ValueTask<List<T>> QueryAsync<T>(Expression<Func<T, bool>> filterPredicate, int skip, int take, Expression<Func<T, object>> orderBy) where T : EntityBase;
    ValueTask Replace<T>(T entity) where T : EntityBase;
}