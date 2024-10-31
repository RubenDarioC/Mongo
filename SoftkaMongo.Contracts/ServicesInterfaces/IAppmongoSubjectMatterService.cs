using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.Contracts.ServicesInterfaces
{
    public interface IAppmongoSubjectMatterService
    {
        void DeleteSubjectMatter(string id);
        List<SubjectMatter> GetAllSubjectMatter();
        SubjectMatter GetSubjectMatter(string id);
        void InsertSubjectMatter(SubjectMatter subjectMatter);
        SubjectMatter UpdateSubjectMatter(SubjectMatter subjectMatter);
    }
}
