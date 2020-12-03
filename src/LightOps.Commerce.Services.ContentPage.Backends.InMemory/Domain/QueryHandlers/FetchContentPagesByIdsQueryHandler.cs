using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.QueryHandlers
{
    public class FetchContentPagesByIdsQueryHandler : IFetchContentPagesByIdsQueryHandler
    {
        private readonly IInMemoryContentPageProvider _inMemoryContentPageProvider;

        public FetchContentPagesByIdsQueryHandler(IInMemoryContentPageProvider inMemoryContentPageProvider)
        {
            _inMemoryContentPageProvider = inMemoryContentPageProvider;
        }
        
        public Task<IList<IContentPage>> HandleAsync(FetchContentPagesByIdsQuery query)
        {
            var contentPages = _inMemoryContentPageProvider
                .ContentPages?
                .Where(c => query.Ids.Contains(c.Id))
                .ToList();

            return Task.FromResult<IList<IContentPage>>(contentPages ?? new List<IContentPage>());
        }
    }
}