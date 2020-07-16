using System.Collections.Generic;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
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

        public Task<IContentPage> GetByIdAsync(string id)
        {
            return _queryDispatcher.DispatchAsync<FetchContentPageByIdQuery, IContentPage>(new FetchContentPageByIdQuery
            {
                Id = id,
            });
        }

        public Task<IContentPage> GetByHandleAsync(string handle)
        {
            return _queryDispatcher.DispatchAsync<FetchContentPageByHandleQuery, IContentPage>(new FetchContentPageByHandleQuery
            {
                Handle = handle,
            });
        }

        public Task<IList<IContentPage>> GetByIdAsync(IList<string> ids)
        {
            return _queryDispatcher.DispatchAsync<FetchContentPagesByIdQuery, IList<IContentPage>>(new FetchContentPagesByIdQuery
            {
                Ids = ids,
            });
        }

        public Task<IList<IContentPage>> GetByHandleAsync(IList<string> handles)
        {
            return _queryDispatcher.DispatchAsync<FetchContentPagesByHandleQuery, IList<IContentPage>>(new FetchContentPagesByHandleQuery
            {
                Handles = handles,
            });
        }

        public Task<IList<IContentPage>> GetByRootAsync()
        {
            return _queryDispatcher.DispatchAsync<FetchContentPagesByRootQuery, IList<IContentPage>>(new FetchContentPagesByRootQuery());
        }

        public Task<IList<IContentPage>> GetByParentIdAsync(string parentId)
        {
            return _queryDispatcher.DispatchAsync<FetchContentPagesByParentIdQuery, IList<IContentPage>>(new FetchContentPagesByParentIdQuery
            {
                ParentId = parentId,
            });
        }

        public Task<IList<IContentPage>> GetBySearchAsync(string searchTerm)
        {
            return _queryDispatcher.DispatchAsync<FetchContentPagesBySearchQuery, IList<IContentPage>>(new FetchContentPagesBySearchQuery
            {
                SearchTerm = searchTerm,
            });
        }
    }
}