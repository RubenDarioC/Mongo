using DataAccessLayer;
using SoftkaMongo.BusinessLogicLayer.Services.AppMongo;
using SoftkaMongo.BusinessLogicLayer.Services.Common;
using SoftkaMongo.Contracts.Common;
using SoftkaMongo.Contracts.RepositoryInterfaces;
using SoftkaMongo.Contracts.ServicesInterfaces;
using SoftkaMongo.DataAccessLayer.Respository;
using SoftkaMongo.Domain.ConfigSettings;

namespace SoftkaMongo.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceDataAccessLayer(this IServiceCollection services)
        {
            services.AddScoped<MongoContext>();
            services.AddScoped<IUnitOfWorkMongo, UnitOfWorkMongo>();
            return services;
        }
        public static IServiceCollection AddServiceContracts(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<ISubjectMatterRepository, SubjectMatterRepository>();

            services.AddScoped<IAppmongoStudentService, AppmongoStudentService>();
            services.AddScoped<IAppmongoProfessorService, AppmongoProfessorService>();
            services.AddScoped<IAppmongoSubjectMatterService, AppmongoSubjectMatterService>();

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
        public static IServiceCollection AddSettingsProviders(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<MongoSettings>(configuration.GetSection(MongoSettings.Position));
            return services;
        }
    }
}
