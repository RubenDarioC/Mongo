using DataAccessLayer;
using SoftkaMongo.Contracts.RepositoryInterfaces;
using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.DataAccessLayer.Respository
{
    public class StudentRepository : RepositoryBase<Students>, IStudentRepository
    {
        public StudentRepository(MongoContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
