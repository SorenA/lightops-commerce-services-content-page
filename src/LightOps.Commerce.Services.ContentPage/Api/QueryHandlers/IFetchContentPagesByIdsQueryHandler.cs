using System.Collections.Generic;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.QueryHandlers
{
    public interface IFetchContentPagesByIdsQueryHandler : IQueryHandler<FetchContentPagesByIdsQuery, IList<Proto.Types.ContentPage>>
    {

    }
}