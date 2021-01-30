using System.Collections.Generic;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers
{
    public interface IInMemoryContentPageProvider
    {
        IList<Proto.Types.ContentPage> ContentPages { get; }
    }
}