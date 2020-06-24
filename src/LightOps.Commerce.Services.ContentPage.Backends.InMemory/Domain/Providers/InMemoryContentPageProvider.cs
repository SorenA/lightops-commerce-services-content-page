using System.Collections.Generic;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.Providers
{
    public class InMemoryContentPageProvider : IInMemoryContentPageProvider
    {
        public IList<IContentPage> ContentPages { get; internal set; }
    }
}