using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.QueryHandlers
{
    public class FetchContentPagesByRootQueryHandler : IFetchContentPagesByRootQueryHandler
    {
        private readonly IInMemoryContentPageProvider _inMemoryContentPageProvider;

        public FetchContentPagesByRootQueryHandler(IInMemoryContentPageProvider inMemoryContentPageProvider)
        {
            _inMemoryContentPageProvider = inMemoryContentPageProvider;
        }

        public Task<IList<IContentPage>> HandleAsync(FetchContentPagesByRootQuery query)
        {
            var contentPages = _inMemoryContentPageProvider
                .ContentPages
                .Where(c => c.ParentContentPageId == null)
                .ToList();

            return Task.FromResult<IList<IContentPage>>(contentPages);
        }
    }
}