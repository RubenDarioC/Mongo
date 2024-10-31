using DataAccessLayer;
using SoftkaMongo.Contracts.RepositoryInterfaces;
using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.DataAccessLayer.Respository
{
    public class SubjectMatterRepository : RepositoryBase<SubjectMatter>, ISubjectMatterRepository
    {
        public SubjectMatterRepository(MongoContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
