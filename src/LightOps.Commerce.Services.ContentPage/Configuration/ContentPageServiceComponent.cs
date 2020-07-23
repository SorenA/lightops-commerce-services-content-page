using System.Collections.Generic;
using System.Linq;
using LightOps.Commerce.Proto.Types;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Api.QueryResults;
using LightOps.Commerce.Services.ContentPage.Api.Services;
using LightOps.Commerce.Services.ContentPage.Domain.Mappers;
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
            ContentPageProtoMapper,
            ImageProtoMapper,
        }

        private readonly Dictionary<Mappers, ServiceRegistration> _mappers = new Dictionary<Mappers, ServiceRegistration>
        {
            [Mappers.ContentPageProtoMapper] = ServiceRegistration.Transient<IMapper<IContentPage, ContentPageProto>, ContentPageProtoMapper>(),
            [Mappers.ImageProtoMapper] = ServiceRegistration.Transient<IMapper<IImage, ImageProto>, ImageProtoMapper>(),
        };

        public IContentPageServiceComponent OverrideContentPageProtoMapper<T>() where T : IMapper<IContentPage, ContentPageProto>
        {
            _mappers[Mappers.ContentPageProtoMapper].ImplementationType = typeof(T);
            return this;
        }

        public IContentPageServiceComponent OverrideImageProtoMapper<T>() where T : IMapper<IImage, ImageProto>
        {
            _mappers[Mappers.ContentPageProtoMapper].ImplementationType = typeof(T);
            return this;
        }

        #endregion Mappers

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

            [QueryHandlers.FetchContentPagesByHandlesQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesByHandlesQuery, IList<IContentPage>>>(),
            [QueryHandlers.FetchContentPagesByIdsQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesByIdsQuery, IList<IContentPage>>>(),
            [QueryHandlers.FetchContentPagesBySearchQueryHandler] = ServiceRegistration.Transient<IQueryHandler<FetchContentPagesBySearchQuery, SearchResult<IContentPage>>>(),
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
    }
}