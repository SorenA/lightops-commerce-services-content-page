using System.Collections.Generic;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.Providers
{
    public class InMemoryContentPageProvider : IInMemoryContentPageProvider
    {
        public IList<Proto.Types.ContentPage> ContentPages { get; internal set; } = new List<Proto.Types.ContentPage>();
    }
}