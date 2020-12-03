using System.Collections.Generic;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Configuration
{
    public interface IInMemoryContentPageServiceBackendComponent
    {
        #region Entities
        IInMemoryContentPageServiceBackendComponent UseContentPages(IList<IContentPage> contentPages);
        #endregion Entities

        #region Providers
        IInMemoryContentPageServiceBackendComponent OverrideContentPageProvider<T>() where T : IInMemoryContentPageProvider;
        #endregion Providers
    }
}