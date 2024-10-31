using DataAccessLayer;
using MongoDB.Driver;
using SoftkaMongo.Contracts.RepositoryInterfaces;
using SoftkaMongo.Domain.ModelsEntities;

namespace SoftkaMongo.DataAccessLayer.Respository
{
    public class ProfessorRepository : RepositoryBase<Professor>, IProfessorRepository
    {
        public ProfessorRepository(MongoContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<Professor> ObtenerEntidadPorNombreApellido(string? surname, string name, string id)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            CancellationToken cancellationToken = cts.Token;

            FilterDefinition<Professor> filter = Builders<Professor>.Filter.And(
            Builders<Professor>.Filter.Eq(e => e.Name, name),
            Builders<Professor>.Filter.Eq(e => e.Surname, surname));

            return await BloquearYObtenerDocumento(filter, cancellationToken);
        }
        public async Task<FilterDefinition<Professor>> FiltroPorNombreYApellido(string? surname, string name)
        {
            FilterDefinition<Professor> filter = Builders<Professor>.Filter.And(
            Builders<Professor>.Filter.Eq(e => e.Name, name),
            Builders<Professor>.Filter.Eq(e => e.Surname, surname));

            return filter;
        }
    }
}
