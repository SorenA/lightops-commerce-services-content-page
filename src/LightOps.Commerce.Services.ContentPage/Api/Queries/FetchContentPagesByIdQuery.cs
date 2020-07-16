using System.Collections.Generic;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.Queries
{
    public class FetchContentPagesByIdQuery : IQuery
    {
        public IList<string> Ids { get; set; }
    }
}