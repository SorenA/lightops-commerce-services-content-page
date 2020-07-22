using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Enums;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Api.QueryResults;
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
        
        public Task<SearchResult<IContentPage>> HandleAsync(FetchContentPagesBySearchQuery query)
        {
            var searchTerm = query.SearchTerm.ToLowerInvariant();

            var inMemoryQuery = _inMemoryContentPageProvider.ContentPages
                .AsQueryable();

            // Sort underlying list
            switch (query.SortKey)
            {
                case ContentPageSortKey.Title:
                    inMemoryQuery = inMemoryQuery.OrderBy(x => x.Title);
                    break;
                case ContentPageSortKey.CreatedAt:
                    inMemoryQuery = inMemoryQuery.OrderBy(x => x.CreatedAt);
                    break;
                case ContentPageSortKey.UpdatedAt:
                    inMemoryQuery = inMemoryQuery.OrderBy(x => x.UpdatedAt);
                    break;
            }

            // Reverse underlying list if requested
            if (query.Reverse)
            {
                inMemoryQuery = inMemoryQuery.Reverse();
            }

            // Match parent id if requested
            if (!string.IsNullOrEmpty(query.ParentId))
            {
                inMemoryQuery = inMemoryQuery.Where(x => x.ParentId == query.ParentId);
            }

            // Search in list
            if (query.SearchTerm != "*")
            {
                inMemoryQuery = inMemoryQuery
                    .Where(x =>
                        (string.IsNullOrWhiteSpace(x.Title) || x.Title.ToLowerInvariant().Contains(searchTerm))
                        || (string.IsNullOrWhiteSpace(x.Summary) || x.Summary.ToLowerInvariant().Contains(searchTerm)));
            }

            // Get total results
            var totalResults = inMemoryQuery.Count();

            // Paginate - Skip
            var nodeId = DecodeCursor(query.PageCursor);
            inMemoryQuery = inMemoryQuery.SkipWhile(x => x.Id != nodeId); // Skip until we reach cursor

            // Get remaining results to know if next page is available
            var remainingResults = inMemoryQuery.Count();

            // Paginate - Take
            var results = inMemoryQuery
                .Take(query.PageSize)
                .ToList();
            
            // Generate next page cursor
            var nextPageCursor = EncodeCursor(results.LastOrDefault()?.Id);

            var searchResult = new SearchResult<IContentPage>
            {
                Results = results,
                NextPageCursor = nextPageCursor,
                HasNextPage = remainingResults > query.PageSize,
                TotalResults = totalResults,
            };

            return Task.FromResult(searchResult);
        }

        private string EncodeCursor(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        private string DecodeCursor(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}