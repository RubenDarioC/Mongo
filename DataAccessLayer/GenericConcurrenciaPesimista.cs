using MongoDB.Driver;
using SoftkaMongo.Contracts.ServicesInterfaces;
using SoftkaMongo.Domain.ModelsEntities;
using System.Reflection;

namespace SoftkaMongo.DataAccessLayer
{
    public class GenericConcurrenciaPesimista  : IGenericConcurrenciaPesimista 
    { 

        public async Task<TMongo> BloquearObtenerDocumentoYDesbloquearActualizar<TMongo>(
            Func<Task<(FilterDefinition<TMongo>, UpdateDefinition<TMongo>)>> filtroparabuscarenmogo,
            Func<TMongo, Task<TMongo>> accionquemodificaralaentidad) where TMongo : BaseDocument
        {
            (FilterDefinition<TMongo>, UpdateDefinition<TMongo>) filterAndUpdate = await filtroparabuscarenmogo();
            MongoClient mongo = new MongoClient("");
            IMongoCollection<TMongo> Collection = mongo.GetDatabase("").GetCollection<TMongo>(typeof(TMongo).GetCustomAttribute<EntityNameAttribute>(true)?.CollectionName ?? typeof(TMongo).Name);
            FilterDefinition<TMongo> filterLockNull = Builders<TMongo>.Filter.And(
                Builders<TMongo>.Filter.Eq(e => e.LockId, null),
                Builders<TMongo>.Filter.Eq(e => e.LockTime, null)
            );

            FilterDefinition<TMongo> filterLockTimeExpired = Builders<TMongo>.Filter.Lt(e => e.LockTime, DateTime.UtcNow);

            FilterDefinition<TMongo> combinedFilter = Builders<TMongo>.Filter.And(
                filterAndUpdate.Item1,
                Builders<TMongo>.Filter.Or(filterLockNull, filterLockTimeExpired)
            );

            UpdateDefinition<TMongo> update = Builders<TMongo>.Update
                    .Set(e => e.LockId, Guid.NewGuid().ToString())
                    .Set(e => e.LockTime, DateTime.UtcNow.AddTicks(200));

            FindOneAndUpdateOptions<TMongo> options = new()
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            TMongo docuemntUpdated;
            try
            {
                TMongo mongoDocument = await Collection.FindOneAndUpdateAsync(combinedFilter, update, options);
                docuemntUpdated = await accionquemodificaralaentidad(mongoDocument);
            }
            catch (Exception ex)
            {
                throw new Exception($"item is already locked");
            }

            FilterDefinition<TMongo> filterUpdated = Builders<TMongo>.Filter
           .And(Builders<TMongo>.Filter.Eq(e => e.LockId, docuemntUpdated.LockId));

            UpdateDefinition<TMongo> lockUpdate = Builders<TMongo>.Update
                    .Set(e => e.LockId, null)
                    .Set(e => e.LockTime, null);

            UpdateDefinition<TMongo> combinedUpdate = Builders<TMongo>.Update.Combine(lockUpdate, filterAndUpdate.Item2);

            try
            {
                return await Collection.FindOneAndUpdateAsync(filterUpdated, combinedUpdate, options);
            }
            catch (MongoCommandException e) when (e.Code == 11000 && e.CodeName == "DuplicateKey")
            {
                throw new Exception($"item is already locked");
            }

        }
    }
}
