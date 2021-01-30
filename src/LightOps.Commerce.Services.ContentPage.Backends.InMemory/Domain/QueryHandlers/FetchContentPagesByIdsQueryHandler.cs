using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        public Task<IList<Proto.Types.ContentPage>> HandleAsync(FetchContentPagesByIdsQuery query)
        {
            var contentPages = _inMemoryContentPageProvider
                .ContentPages?
                .Where(c => query.Ids.Contains(c.Id))
                .ToList();

            return Task.FromResult<IList<Proto.Types.ContentPage>>(contentPages ?? new List<Proto.Types.ContentPage>());
        }
    }
}