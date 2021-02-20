using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.QueryHandlers
{
    public class FetchContentPagesByHandlesQueryHandler : IFetchContentPagesByHandlesQueryHandler
    {
        private readonly IInMemoryContentPageProvider _inMemoryContentPageProvider;

        public FetchContentPagesByHandlesQueryHandler(IInMemoryContentPageProvider inMemoryContentPageProvider)
        {
            _inMemoryContentPageProvider = inMemoryContentPageProvider;
        }

        public Task<IList<Proto.Types.ContentPage>> HandleAsync(FetchContentPagesByHandlesQuery query)
        {
            // Match any localized handle
            var contentPages = _inMemoryContentPageProvider
                .ContentPages?
                .Where(c => c.Handles
                    .Select(ls => ls.Value)
                    .Intersect(query.Handles)
                    .Any())
                .ToList();

            return Task.FromResult<IList<Proto.Types.ContentPage>>(contentPages ?? new List<Proto.Types.ContentPage>());
        }
    }
}