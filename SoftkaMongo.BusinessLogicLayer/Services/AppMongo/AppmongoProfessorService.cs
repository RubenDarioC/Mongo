using SoftkaMongo.Contracts.ServicesInterfaces;
using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.BusinessLogicLayer.Services.AppMongo
{
    public class AppmongoProfessorService : BaseServices<AppmongoProfessorService>, IAppmongoProfessorService
    {
        public AppmongoProfessorService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public List<Professor> GetAllProfessor()
        {
            return UnitOfWorkMongo.Professor.GetAll().ToList();
        }
        public void InsertProfessor(Professor professor)
        {
            UnitOfWorkMongo.Professor.Insert(professor);
        }
        public void DeleteProfessor(string idProfessor)
        {
            UnitOfWorkMongo.Professor.Remove(idProfessor);
        }
        public Professor GetProfessor(string idProfessor)
        {
            return UnitOfWorkMongo.Professor.GetById(idProfessor);
        }
        public Professor UpdateProfessor(Professor professor)
        {
            UnitOfWorkMongo.Professor.Update(professor, professor.Id);
            Professor result = UnitOfWorkMongo.Professor.GetById(professor.Id);
            return result;
        }
    }
}
