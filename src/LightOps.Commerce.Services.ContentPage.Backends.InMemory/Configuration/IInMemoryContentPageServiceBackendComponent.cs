using System.Collections.Generic;
using LightOps.Commerce.Services.ContentPage.Api.Models;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Configuration
{
    public interface IInMemoryContentPageServiceBackendComponent
    {
        #region Entities
        IInMemoryContentPageServiceBackendComponent UseContentPages(IList<IContentPage> contentPages);
        #endregion Entities
    }
}