using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SoftkaMongo.Contracts.Common;
using SoftkaMongo.Contracts.RepositoryInterfaces;
using System;

namespace SoftkaMongo.BusinessLogicLayer
{
    public abstract class BaseServices<TClass>
    {
        protected ILogger<TClass> Logger { get; set; }
        public IServiceProvider ServiceProvider { get; }
        protected readonly IUnitOfWorkMongo UnitOfWorkMongo;
        protected readonly IDateTimeProvider DateTimeProvider;
        protected BaseServices(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            this.Logger = ActivatorUtilities.GetServiceOrCreateInstance<ILogger<TClass>>(serviceProvider);
            this.UnitOfWorkMongo = ActivatorUtilities.GetServiceOrCreateInstance<IUnitOfWorkMongo>(serviceProvider);
            this.DateTimeProvider = ActivatorUtilities.GetServiceOrCreateInstance<IDateTimeProvider>(serviceProvider);
        }
    }
}
