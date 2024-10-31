using SoftkaMongo.Contracts.ServicesInterfaces;
using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.BusinessLogicLayer.Services.AppMongo
{
    /// <summary>
    /// Student partial class 
    /// </summary>
    public class AppmongoStudentService : BaseServices<AppmongoStudentService>, IAppmongoStudentService
    {
        public AppmongoStudentService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public List<Students> GetAllStudents()
        {
            return UnitOfWorkMongo.Student.GetAll().ToList();
        }
        public void InsertStudent(Students students)
        {
            UnitOfWorkMongo.Student.Insert(students);
        }
        public void DeleteStudent(string students)
        {
            UnitOfWorkMongo.Student.Remove(students);
        }

        public Students GetStudent(string id)
        {
            return UnitOfWorkMongo.Student.GetById(id);
        }

        public Students UpdateStudent(Students students)
        {
            UnitOfWorkMongo.Student.Update(students, students.Id);
            return new Students { };
        }
    }
}
