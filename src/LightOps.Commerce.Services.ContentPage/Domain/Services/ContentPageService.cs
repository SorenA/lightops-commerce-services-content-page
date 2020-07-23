using System.Collections.Generic;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Enums;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryResults;
using LightOps.Commerce.Services.ContentPage.Api.Services;
using LightOps.CQRS.Api.Services;

namespace LightOps.Commerce.Services.ContentPage.Domain.Services
{
    public class ContentPageService : IContentPageService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public ContentPageService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public Task<IList<IContentPage>> GetByHandleAsync(IList<string> handles)
        {
            return _queryDispatcher.DispatchAsync<FetchContentPagesByHandlesQuery, IList<IContentPage>>(new FetchContentPagesByHandlesQuery
            {
                Handles = handles,
            });
        }

        public Task<IList<IContentPage>> GetByIdAsync(IList<string> ids)
        {
            return _queryDispatcher.DispatchAsync<FetchContentPagesByIdsQuery, IList<IContentPage>>(new FetchContentPagesByIdsQuery
            {
                Ids = ids,
            });
        }

        public Task<SearchResult<IContentPage>> GetBySearchAsync(string searchTerm,
                                                                 string parentId = null,
                                                                 string pageCursor = null,
                                                                 int pageSize = 24,
                                                                 ContentPageSortKey sortKey = ContentPageSortKey.Default,
                                                                 bool reverse = false)
        {
            return _queryDispatcher.DispatchAsync<FetchContentPagesBySearchQuery, SearchResult<IContentPage>>(
                new FetchContentPagesBySearchQuery
                {
                    SearchTerm = searchTerm,
                    ParentId = parentId,
                    PageCursor = pageCursor,
                    PageSize = pageSize,
                    SortKey = sortKey,
                    Reverse = reverse,
                });
        }
    }
}