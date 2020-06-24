using System.Collections.Generic;
using LightOps.Commerce.Services.ContentPage.Api.Models;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers
{
    public interface IInMemoryContentPageProvider
    {
        IList<IContentPage> ContentPages { get; }
    }
}