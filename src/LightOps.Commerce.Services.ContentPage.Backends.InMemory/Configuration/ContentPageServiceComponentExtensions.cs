using System;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Configuration;
using LightOps.DependencyInjection.Configuration;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Configuration
{
    public static class ContentPageServiceComponentExtensions
    {
        public static IContentPageServiceComponent UseInMemoryBackend(
            this IContentPageServiceComponent serviceComponent,
            IDependencyInjectionRootComponent rootComponent,
            Action<IInMemoryContentPageServiceBackendComponent> componentConfig = null)
        {
            var component = new InMemoryContentPageServiceBackendComponent();

            // Invoke config delegate
            componentConfig?.Invoke(component);

            // Attach to root component
            rootComponent.AttachComponent(component);

            // Override query handlers
            serviceComponent
                .OverrideCheckContentPageHealthQueryHandler<CheckContentPageHealthQueryHandler>()
                .OverrideFetchContentPagesByParentIdQueryHandler<FetchContentPagesByParentIdQueryHandler>()
                .OverrideFetchContentPagesByRootQueryHandler<FetchContentPagesByRootQueryHandler>()
                .OverrideFetchContentPagesByHandlesQueryHandler<FetchContentPagesByHandlesQueryHandler>()
                .OverrideFetchContentPagesByIdsQueryHandler<FetchContentPagesByIdsQueryHandler>()
                .OverrideFetchContentPageByHandleQueryHandler<FetchContentPageByHandleQueryHandler>()
                .OverrideFetchContentPageByIdQueryHandler<FetchContentPageByIdQueryHandler>()
                .OverrideFetchContentPagesBySearchQueryHandler<FetchContentPagesBySearchQueryHandler>();

            return serviceComponent;
        }
    }
}
