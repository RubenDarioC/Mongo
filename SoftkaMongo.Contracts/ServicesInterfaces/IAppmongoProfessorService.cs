using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.Contracts.ServicesInterfaces
{
    public interface IAppmongoProfessorService
    {
        void DeleteProfessor(string idProfessor);
        List<Professor> GetAllProfessor();
        Professor GetProfessor(string idProfessor);
        void InsertProfessor(Professor professor);
        Professor UpdateProfessor(Professor professor);
    }
}
