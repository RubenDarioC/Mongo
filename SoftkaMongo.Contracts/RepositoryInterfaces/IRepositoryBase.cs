
namespace SoftkaMongo.Contracts.RepositoryInterfaces
{
    public interface IRepositoryBase<T>
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        void Insert(T document);
        void Remove(string id);
        void Update(T obj, string id);
    }
}