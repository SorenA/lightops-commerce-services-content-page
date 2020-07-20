using System.Collections.Generic;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.Queries
{
    public class FetchContentPagesByParentIdsQuery : IQuery
    {
        public IList<string> ParentIds { get; set; }
    }
}