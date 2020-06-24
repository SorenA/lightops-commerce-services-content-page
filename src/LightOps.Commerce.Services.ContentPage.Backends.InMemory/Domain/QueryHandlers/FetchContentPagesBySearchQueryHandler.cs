using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.QueryHandlers
{
    public class FetchContentPagesBySearchQueryHandler : IFetchContentPagesBySearchQueryHandler
    {
        private readonly IInMemoryContentPageProvider _inMemoryContentPageProvider;

        public FetchContentPagesBySearchQueryHandler(IInMemoryContentPageProvider inMemoryContentPageProvider)
        {
            _inMemoryContentPageProvider = inMemoryContentPageProvider;
        }
        
        public Task<IList<IContentPage>> HandleAsync(FetchContentPagesBySearchQuery query)
        {
            var searchTerm = query.SearchTerm.ToLowerInvariant();

            var contentPageQuery = _inMemoryContentPageProvider.ContentPages
                .AsQueryable();

            if (query.SearchTerm != "*")
            {
                contentPageQuery = contentPageQuery
                    .Where(c =>
                        (string.IsNullOrWhiteSpace(c.Title) || c.Title.ToLowerInvariant().Contains(searchTerm))
                        || (string.IsNullOrWhiteSpace(c.Description) || c.Description.ToLowerInvariant().Contains(searchTerm)));
            }

            return Task.FromResult<IList<IContentPage>>(contentPageQuery.ToList());
        }
    }
}