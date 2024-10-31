using SoftkaMongo.Contracts.ServicesInterfaces;
using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.BusinessLogicLayer.Services.AppMongo
{
    /// <summary>
    /// Student partial class 
    /// </summary>
    public class AppmongoSubjectMatterService : BaseServices<AppmongoSubjectMatterService>, IAppmongoSubjectMatterService
    {
        public AppmongoSubjectMatterService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public List<SubjectMatter> GetAllSubjectMatter()
        {
            return UnitOfWorkMongo.SubjectMatter.GetAll().ToList();
        }
        public void InsertSubjectMatter(SubjectMatter subjectMatter)
        {
            UnitOfWorkMongo.SubjectMatter.Insert(subjectMatter);
        }
        public void DeleteSubjectMatter(string id)
        {
            UnitOfWorkMongo.SubjectMatter.Remove(id);
        }
        public SubjectMatter GetSubjectMatter(string id)
        {
            return UnitOfWorkMongo.SubjectMatter.GetById(id);
        }
        public SubjectMatter UpdateSubjectMatter(SubjectMatter subjectMatter)
        {
            UnitOfWorkMongo.SubjectMatter.Update(subjectMatter, subjectMatter.Id);
            SubjectMatter result = UnitOfWorkMongo.SubjectMatter.GetById(subjectMatter.Id);
            return result;
        }
    }
}
