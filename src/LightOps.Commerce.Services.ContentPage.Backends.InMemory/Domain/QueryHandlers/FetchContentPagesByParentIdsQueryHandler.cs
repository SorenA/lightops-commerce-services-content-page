using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.QueryHandlers
{
    public class FetchContentPagesByParentIdsQueryHandler : IFetchContentPagesByParentIdsQueryHandler
    {
        private readonly IInMemoryContentPageProvider _inMemoryContentPageProvider;

        public FetchContentPagesByParentIdsQueryHandler(IInMemoryContentPageProvider inMemoryContentPageProvider)
        {
            _inMemoryContentPageProvider = inMemoryContentPageProvider;
        }
        
        public Task<IList<IContentPage>> HandleAsync(FetchContentPagesByParentIdsQuery query)
        {
            var contentPages = _inMemoryContentPageProvider
                .ContentPages
                .Where(c => query.ParentIds.Contains(c.ParentContentPageId))
                .ToList();

            return Task.FromResult<IList<IContentPage>>(contentPages);
        }
    }
}