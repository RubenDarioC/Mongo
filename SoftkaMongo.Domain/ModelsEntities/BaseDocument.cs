
namespace SoftkaMongo.Domain.ModelsEntities
{
    public abstract class BaseDocument
    {
        public string Id { get; set; } = null!;
        public string? LockId { get; set; }
        public DateTime? LockTime { get; set; }
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class EntityNameAttribute : Attribute
    {
        public EntityNameAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
        public string CollectionName { get; set; }
    }
}
