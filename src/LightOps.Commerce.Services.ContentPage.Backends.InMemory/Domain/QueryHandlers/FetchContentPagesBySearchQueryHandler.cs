using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Proto.Types;
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
        
        public Task<SearchResult<Proto.Types.ContentPage>> HandleAsync(FetchContentPagesBySearchQuery query)
        {
            var inMemoryQuery = _inMemoryContentPageProvider
                .ContentPages?
                .AsQueryable() ?? new EnumerableQuery<Proto.Types.ContentPage>(new List<Proto.Types.ContentPage>());

            // Filter out un-searchable
            inMemoryQuery = inMemoryQuery.Where(x => x.IsSearchable);

            // Use default sorting order
            inMemoryQuery = inMemoryQuery.OrderBy(x => x.SortOrder);

            // Sort underlying list if sorting key is specified
            switch (query.SortKey)
            {
                case ContentPageSortKey.Title:
                    // Only possible when using a language code
                    if (!string.IsNullOrEmpty(query.LanguageCode))
                    {
                        inMemoryQuery = inMemoryQuery.OrderBy(x => x.Titles
                            .Where(l => l.LanguageCode == query.LanguageCode) // Match language code
                            .Select(l => l.Value) // Convert to value
                            .FirstOrDefault());
                    }
                    break;
                case ContentPageSortKey.CreatedAt:
                    inMemoryQuery = inMemoryQuery.OrderBy(x => x.CreatedAt.ToDateTime());
                    break;
                case ContentPageSortKey.UpdatedAt:
                    inMemoryQuery = inMemoryQuery.OrderBy(x => x.UpdatedAt.ToDateTime());
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
            if (!string.IsNullOrEmpty(query.SearchTerm))
            {
                // Match language if requested
                if (!string.IsNullOrEmpty(query.LanguageCode))
                {
                    inMemoryQuery = inMemoryQuery
                        .Where(x =>
                            x.Titles.Any(l =>
                                l.LanguageCode == query.LanguageCode
                                && l.Value.ToLowerInvariant().Contains(query.SearchTerm,
                                    StringComparison.InvariantCultureIgnoreCase))
                            || x.Descriptions.Any(l =>
                                l.LanguageCode == query.LanguageCode
                                && l.Value.ToLowerInvariant().Contains(query.SearchTerm,
                                    StringComparison.InvariantCultureIgnoreCase)));
                }
                else
                {
                    // No language code, match all
                    inMemoryQuery = inMemoryQuery
                        .Where(x =>
                            x.Titles.Any(l =>
                                l.Value.ToLowerInvariant().Contains(query.SearchTerm,
                                    StringComparison.InvariantCultureIgnoreCase))
                            || x.Descriptions.Any(l =>
                                l.Value.ToLowerInvariant().Contains(query.SearchTerm,
                                    StringComparison.InvariantCultureIgnoreCase)));
                }
            }

            // Get total results
            var totalResults = inMemoryQuery.Count();

            // Paginate - Skip
            var nodeId = DecodeCursor(query.PageCursor);
            var remainingResultsPrePagination = inMemoryQuery.Count();
            if (!string.IsNullOrEmpty(nodeId))
            {
                // Skip until we reach cursor, then one more for next page
                inMemoryQuery = inMemoryQuery
                    .SkipWhile(x => x.Id != nodeId)
                    .Skip(1);
            }

            // Get remaining results to know if next page is available
            var remainingResults = inMemoryQuery.Count();

            // Paginate - Take
            var results = inMemoryQuery
                .Take(query.PageSize)
                .Select(x => new CursorNodeResult<Proto.Types.ContentPage>
                {
                    Cursor = EncodeCursor(x.Id),
                    Node = x,
                })
                .ToList();

            // Get cursors
            var startCursor = results.FirstOrDefault()?.Cursor;
            var endCursor = results.LastOrDefault()?.Cursor;

            var searchResult = new SearchResult<Proto.Types.ContentPage>
            {
                Results = results,
                StartCursor = startCursor,
                EndCursor = endCursor,
                HasNextPage = remainingResults > query.PageSize,
                HasPreviousPage = remainingResultsPrePagination != remainingResults,
                TotalResults = totalResults,
            };

            return Task.FromResult(searchResult);
        }

        private string EncodeCursor(string rawCursor)
        {
            try
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(rawCursor);
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                // Invalid cursor
                return string.Empty;
            }
        }

        private string DecodeCursor(string encodedCursor)
        {
            try
            {
                var bytes = Convert.FromBase64String(encodedCursor);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                // Invalid cursor
                return string.Empty;
            }
        }
    }
}