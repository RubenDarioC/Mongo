namespace SoftkaMongo.Contracts.RepositoryInterfaces
{
    public interface IUnitOfWorkMongo : IDisposable
    {
        IStudentRepository Student { get; }
        IProfessorRepository Professor { get; }
        ISubjectMatterRepository SubjectMatter { get; }
        #region UnitMethods
        void AddCommand(Func<Task> func);
        Task<int> SaveChanges();
        #endregion

    }
}
