using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.Contracts.ServicesInterfaces
{
    public interface IAppmongoStudentService
    {
        void DeleteStudent(string students);
        List<Students> GetAllStudents();
        void InsertStudent(Students students);
        Students GetStudent(string id);
        Students UpdateStudent(Students students);
    }
}
