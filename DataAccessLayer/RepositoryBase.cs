using MongoDB.Bson;
using MongoDB.Driver;
using SoftkaMongo.Contracts.RepositoryInterfaces;
using SoftkaMongo.Domain.ModelsEntities;
using System.Reflection;

namespace DataAccessLayer;
public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseDocument
{
    protected readonly MongoContext Context;
    protected readonly IMongoCollection<T> Collection;
    protected RepositoryBase(MongoContext repositoryContext)
    {
        Context = repositoryContext;
        Collection = Context.Database.GetCollection<T>(typeof(T).GetCustomAttribute<EntityNameAttribute>(true)?.CollectionName ?? typeof(T).Name);
    }

    public T GetById(string id)
    {
        var data = Collection.Find(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)));
        return data.FirstOrDefault();
    }
    public void Insert(T document)
    {
        Collection.InsertOneAsync(document);
    }
    public virtual IEnumerable<T> GetAll()
    {
        var all = Collection.Find(Builders<T>.Filter.Empty);
        return all.ToList();
    }
    public virtual void Remove(string id)
    {
        Collection.DeleteOne(Builders<T>.Filter.Eq("_id", ObjectId.Parse(id)));
    }
    public virtual void Update(T obj, string id)
    {
        Collection.ReplaceOne(Builders<T>.Filter.Eq(e => e.Id, id), obj);
    }
    public async Task<T> BloquearYObtenerDocumento(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
    {

        FilterDefinition<T> filterLockNull = Builders<T>.Filter.And(
            Builders<T>.Filter.Eq(e => e.LockId, null),
            Builders<T>.Filter.Eq(e => e.LockTime, null)
        );

        FilterDefinition<T> filterLockTimeExpired = Builders<T>.Filter.Lt(e => e.LockTime, DateTime.UtcNow);

        FilterDefinition<T> combinedFilter = Builders<T>.Filter.And(
            filter,
            Builders<T>.Filter.Or(filterLockNull, filterLockTimeExpired)
        );

        UpdateDefinition<T> update = Builders<T>.Update
                .Set(e => e.LockId, Guid.NewGuid().ToString())
                .Set(e => e.LockTime, DateTime.UtcNow.AddTicks(200));

        FindOneAndUpdateOptions<T> options = new()
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };

        try
        {
            return await Collection.FindOneAndUpdateAsync(combinedFilter, update, options, cancellationToken);
        }
        catch (MongoCommandException e) when (e.Code == 11000 && e.CodeName == "DuplicateKey")
        {
            throw new Exception($"item is already locked");
        }
    }

    public async Task<T> DesbloquearYActualizarDocumento(string lockId, UpdateDefinition<T> updatedefinition, CancellationToken cancellationToken = default)
    {
        FilterDefinition<T> filter = Builders<T>.Filter
            .And(Builders<T>.Filter.Eq(e => e.LockId, lockId));

        UpdateDefinition<T> lockUpdate = Builders<T>.Update
                .Set(e => e.LockId, null)
                .Set(e => e.LockTime, null);
    
        UpdateDefinition<T> combinedUpdate = Builders<T>.Update.Combine(lockUpdate, updatedefinition);
        FindOneAndUpdateOptions<T> options = new()
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };

        try
        {

            return await Collection.FindOneAndUpdateAsync(filter, combinedUpdate, options, cancellationToken);
        }
        catch (MongoCommandException e) when (e.Code == 11000 && e.CodeName == "DuplicateKey")
        {
            throw new Exception($"item is already locked");
        }
    }
}
