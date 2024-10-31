using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using SoftkaMongo.Domain.ConfigSettings;

namespace DataAccessLayer
{
    public class MongoContext
    {
        public readonly List<Func<Task>> Commands;
        public IMongoDatabase Database { get; set; }
        public MongoClient MongoClient { get; set; }
        public IClientSessionHandle Session { get; set; } = null!;


        public MongoContext(IOptions<MongoSettings> settings)
        {
            var pack = new ConventionPack { 
                new StringIdStoredAsObjectIdConvention(),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
            };
            ConventionRegistry.Register("Custom Convention", pack, t => true);
            
            this.Commands = new List<Func<Task>>();
            this.MongoClient = new MongoClient(settings.Value.Connection);
            this.Database = MongoClient.GetDatabase(settings.Value.DataBaseName);
        }
    }
}
