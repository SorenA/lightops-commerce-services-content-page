using System.Collections.Generic;
using System.Linq;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.Providers;
using LightOps.DependencyInjection.Api.Configuration;
using LightOps.DependencyInjection.Domain.Configuration;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Configuration
{
    public class InMemoryContentPageServiceBackendComponent : IDependencyInjectionComponent, IInMemoryContentPageServiceBackendComponent
    {
        public string Name => "lightops.commerce.services.content-page.backend.in-memory";

        public IReadOnlyList<ServiceRegistration> GetServiceRegistrations()
        {
            return new List<ServiceRegistration>()
                .Union(_providers.Values)
                .ToList();
        }

        #region Entities
        public IInMemoryContentPageServiceBackendComponent UseContentPages(IList<IContentPage> contentPages)
        {
            // Populate in-memory providers
            _providers[Providers.InMemoryContentPageProvider].ImplementationInstance = new InMemoryContentPageProvider
            {
                ContentPages = contentPages,
            };

            return this;
        }
        #endregion Entities

        #region Providers
        internal enum Providers
        {
            InMemoryContentPageProvider,
        }

        private readonly Dictionary<Providers, ServiceRegistration> _providers = new Dictionary<Providers, ServiceRegistration>()
        {
            [Providers.InMemoryContentPageProvider] = ServiceRegistration.Singleton<IInMemoryContentPageProvider, InMemoryContentPageProvider>(),
        };

        public IInMemoryContentPageServiceBackendComponent OverrideContentPageProvider<T>() where T : IInMemoryContentPageProvider
        {
            _providers[Providers.InMemoryContentPageProvider].ImplementationType = typeof(T);
            return this;
        }
        #endregion Providers
    }
}