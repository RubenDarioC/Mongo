using SoftkaMongo.Contracts.RepositoryInterfaces;
using SoftkaMongo.DataAccessLayer.Respository;

namespace DataAccessLayer
{
    public class UnitOfWorkMongo : IUnitOfWorkMongo
    {
        private readonly MongoContext MongoContext;
        public UnitOfWorkMongo(MongoContext mongoContext)
        {
            this.MongoContext = mongoContext;
        }
        #region Repositories
        private IStudentRepository _student = null!;
        public IStudentRepository Student
        {
            get { return _student ??= new StudentRepository(MongoContext); }
        }
        private IProfessorRepository _professor = null!;
        public IProfessorRepository Professor
        {
            get { return _professor ??= new ProfessorRepository(MongoContext); }
        }

        private ISubjectMatterRepository _subjectMatter = null!;
        public ISubjectMatterRepository SubjectMatter
        {
            get { return _subjectMatter ??= new SubjectMatterRepository(MongoContext); }
        }

        #endregion

        public async Task<int> SaveChanges()
        {

            using (MongoContext.Session = await MongoContext.MongoClient.StartSessionAsync())
            {
                MongoContext.Session.StartTransaction();
                var commandTasks = MongoContext.Commands.Select(c => c());
                await Task.WhenAll(commandTasks);
                await MongoContext.Session.CommitTransactionAsync();
            }
            return MongoContext.Commands.Count;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            MongoContext.Session?.Dispose();
        }
        public void AddCommand(Func<Task> func)
        {
            MongoContext.Commands.Add(func);
        }
    }
}
