using System.Collections.Generic;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.QueryResults;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.QueryHandlers
{
    public interface IFetchContentPagesBySearchQueryHandler : IQueryHandler<FetchContentPagesBySearchQuery, SearchResult<IContentPage>>
    {

    }
}