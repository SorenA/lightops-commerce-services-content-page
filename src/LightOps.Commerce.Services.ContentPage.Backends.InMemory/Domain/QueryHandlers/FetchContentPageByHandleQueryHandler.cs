using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.QueryHandlers
{
    public class FetchContentPageByHandleQueryHandler : IFetchContentPageByHandleQueryHandler
    {
        private readonly IInMemoryContentPageProvider _inMemoryContentPageProvider;

        public FetchContentPageByHandleQueryHandler(IInMemoryContentPageProvider inMemoryContentPageProvider)
        {
            _inMemoryContentPageProvider = inMemoryContentPageProvider;
        }

        public Task<IContentPage> HandleAsync(FetchContentPageByHandleQuery query)
        {
            var contentPage = _inMemoryContentPageProvider
                .ContentPages
                .FirstOrDefault(c => c.Handle == query.Handle);

            return Task.FromResult(contentPage);
        }
    }
}