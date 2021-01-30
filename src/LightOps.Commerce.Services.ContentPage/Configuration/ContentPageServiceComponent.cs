using System.Collections.Generic;
using System.Linq;
using LightOps.Commerce.Services.ContentPage.Api.CommandHandlers;
using LightOps.Commerce.Services.ContentPage.Api.Commands;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Api.QueryResults;
using LightOps.CQRS.Api.Commands;
using LightOps.CQRS.Api.Queries;
using LightOps.DependencyInjection.Api.Configuration;
using LightOps.DependencyInjection.Domain.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.ContentPage.Configuration
{
    public class ContentPageServiceComponent : IDependencyInjectionComponent, IContentPageServiceComponent
    {
        public string Name => "lightops.commerce.services.content-page";

        public IReadOnlyList<ServiceRegistration> GetServiceRegistrations()
        {
            return new List<ServiceRegistration>()
                .Union(_queryHandlers.Values)
                .Union(_commandHandlers.Values)
                .ToList();
        }

        #region Query Handlers
        internal enum QueryHandlers
        {
            CheckContentPageHealthQueryHandler,

            FetchContentPagesByHandlesQueryHandler,
            FetchContentPagesByIdsQueryHandler,
            FetchContentPagesBySearchQueryHandler,
        }

        private readonly Dictionary<QueryHandlers, ServiceRegistration> _queryHandlers = new Dictionary<QueryHandlers, ServiceRegistration>
        {
            [QueryHandlers.CheckContentPageHealthQueryHandler] = ServiceRegistration.Transient<IQueryHandler<CheckContentPageHealthQuery, HealthStatus>>(),

            [QueryHandlers.FetchContentPagesByHandlesQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesByHandlesQuery, IList<Proto.Types.ContentPage>>>(),
            [QueryHandlers.FetchContentPagesByIdsQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesByIdsQuery, IList<Proto.Types.ContentPage>>>(),
            [QueryHandlers.FetchContentPagesBySearchQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesBySearchQuery, SearchResult<Proto.Types.ContentPage>>>(),
        };

        public IContentPageServiceComponent OverrideCheckContentPageHealthQueryHandler<T>() where T : ICheckContentPageHealthQueryHandler
        {
            _queryHandlers[QueryHandlers.CheckContentPageHealthQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPagesByHandlesQueryHandler<T>() where T : IFetchContentPagesByHandlesQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPagesByHandlesQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPagesByIdsQueryHandler<T>() where T : IFetchContentPagesByIdsQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPagesByIdsQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPagesBySearchQueryHandler<T>() where T : IFetchContentPagesBySearchQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPagesBySearchQueryHandler].ImplementationType = typeof(T);
            return this;
        }
        #endregion Query Handlers

        #region Command Handlers
        internal enum CommandHandlers
        {
            PersistContentPageCommandHandler,
            DeleteContentPageCommandHandler,
        }

        private readonly Dictionary<CommandHandlers, ServiceRegistration> _commandHandlers = new Dictionary<CommandHandlers, ServiceRegistration>
        {
            [CommandHandlers.PersistContentPageCommandHandler] = ServiceRegistration.Transient<ICommandHandler<PersistContentPageCommand>>(),
            [CommandHandlers.DeleteContentPageCommandHandler] = ServiceRegistration.Transient<ICommandHandler<DeleteContentPageCommand>>(),
        };

        public IContentPageServiceComponent OverridePersistContentPageCommandHandler<T>() where T : IPersistContentPageCommandHandler
        {
            _commandHandlers[CommandHandlers.PersistContentPageCommandHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideDeleteContentPageCommandHandler<T>() where T : IDeleteContentPageCommandHandler
        {
            _commandHandlers[CommandHandlers.DeleteContentPageCommandHandler].ImplementationType = typeof(T);
            return this;
        }
        #endregion Command Handlers
    }
}