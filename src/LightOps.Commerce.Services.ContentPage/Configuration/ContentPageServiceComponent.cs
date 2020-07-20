using System.Collections.Generic;
using System.Linq;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Api.Services;
using LightOps.Commerce.Services.ContentPage.Domain.Mappers.V1;
using LightOps.Commerce.Services.ContentPage.Domain.Services;
using LightOps.CQRS.Api.Queries;
using LightOps.DependencyInjection.Api.Configuration;
using LightOps.DependencyInjection.Domain.Configuration;
using LightOps.Mapping.Api.Mappers;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.ContentPage.Configuration
{
    public class ContentPageServiceComponent : IDependencyInjectionComponent, IContentPageServiceComponent
    {
        public string Name => "lightops.commerce.services.content-page";

        public IReadOnlyList<ServiceRegistration> GetServiceRegistrations()
        {
            return new List<ServiceRegistration>()
                .Union(_services.Values)
                .Union(_mappers.Values)
                .Union(_queryHandlers.Values)
                .ToList();
        }

        #region Services
        internal enum Services
        {
            HealthService,
            ContentPageService,
        }

        private readonly Dictionary<Services, ServiceRegistration> _services = new Dictionary<Services, ServiceRegistration>
        {
            [Services.HealthService] = ServiceRegistration.Transient<IHealthService, HealthService>(),
            [Services.ContentPageService] = ServiceRegistration.Transient<IContentPageService, ContentPageService>(),
        };

        public IContentPageServiceComponent OverrideHealthService<T>()
            where T : IHealthService
        {
            _services[Services.HealthService].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideContentPageService<T>()
            where T : IContentPageService
        {
            _services[Services.ContentPageService].ImplementationType = typeof(T);
            return this;
        }
        #endregion Services

        #region Mappers
        internal enum Mappers
        {
            ProtoContentPageMapperV1,
        }

        private readonly Dictionary<Mappers, ServiceRegistration> _mappers = new Dictionary<Mappers, ServiceRegistration>
        {
            [Mappers.ProtoContentPageMapperV1] = ServiceRegistration
                .Transient<IMapper<IContentPage, Proto.Services.ContentPage.V1.ProtoContentPage>, ProtoContentPageMapper>(),
        };

        public IContentPageServiceComponent OverrideProtoContentPageMapperV1<T>() where T : IMapper<IContentPage, Proto.Services.ContentPage.V1.ProtoContentPage>
        {
            _mappers[Mappers.ProtoContentPageMapperV1].ImplementationType = typeof(T);
            return this;
        }
        #endregion Mappers

        #region Query Handlers
        internal enum QueryHandlers
        {
            CheckContentPageHealthQueryHandler,

            FetchContentPageByIdQueryHandler,
            FetchContentPagesByIdsQueryHandler,

            FetchContentPageByHandleQueryHandler,
            FetchContentPagesByHandlesQueryHandler,

            FetchContentPagesByParentIdQueryHandler,
            FetchContentPagesByParentIdsQueryHandler,

            FetchContentPagesByRootQueryHandler,
            FetchContentPagesBySearchQueryHandler,
        }

        private readonly Dictionary<QueryHandlers, ServiceRegistration> _queryHandlers = new Dictionary<QueryHandlers, ServiceRegistration>
        {
            [QueryHandlers.CheckContentPageHealthQueryHandler] = ServiceRegistration.Transient<IQueryHandler<CheckContentPageHealthQuery, HealthStatus>>(),

            [QueryHandlers.FetchContentPageByIdQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPageByIdQuery, IContentPage>>(),
            [QueryHandlers.FetchContentPagesByIdsQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesByIdsQuery, IList<IContentPage>>>(),

            [QueryHandlers.FetchContentPageByHandleQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPageByHandleQuery, IContentPage>>(),
            [QueryHandlers.FetchContentPagesByHandlesQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesByHandlesQuery, IList<IContentPage>>>(),

            [QueryHandlers.FetchContentPagesByParentIdQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesByParentIdQuery, IList<IContentPage>>>(),
            [QueryHandlers.FetchContentPagesByParentIdsQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesByParentIdsQuery, IList<IContentPage>>>(),

            [QueryHandlers.FetchContentPagesByRootQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesByRootQuery, IList<IContentPage>>>(),
            [QueryHandlers.FetchContentPagesBySearchQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesBySearchQuery, IList<IContentPage>>>(),
        };

        public IContentPageServiceComponent OverrideCheckContentPageHealthQueryHandler<T>() where T : ICheckContentPageHealthQueryHandler
        {
            _queryHandlers[QueryHandlers.CheckContentPageHealthQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPageByIdQueryHandler<T>() where T : IFetchContentPageByIdQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPageByIdQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPagesByIdsQueryHandler<T>() where T : IFetchContentPagesByIdsQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPagesByIdsQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPageByHandleQueryHandler<T>() where T : IFetchContentPageByHandleQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPageByHandleQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPagesByHandlesQueryHandler<T>() where T : IFetchContentPagesByHandlesQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPagesByHandlesQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPagesByParentIdQueryHandler<T>() where T : IFetchContentPagesByParentIdQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPagesByParentIdQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPagesByParentIdsQueryHandler<T>() where T : IFetchContentPagesByParentIdsQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPagesByParentIdsQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPagesByRootQueryHandler<T>() where T : IFetchContentPagesByRootQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPagesByRootQueryHandler].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideFetchContentPagesBySearchQueryHandler<T>() where T : IFetchContentPagesBySearchQueryHandler
        {
            _queryHandlers[QueryHandlers.FetchContentPagesBySearchQueryHandler].ImplementationType = typeof(T);
            return this;
        }
        #endregion Query Handlers
    }
}