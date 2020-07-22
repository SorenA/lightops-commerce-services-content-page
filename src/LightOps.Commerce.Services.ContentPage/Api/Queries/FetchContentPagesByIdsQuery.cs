using System.Collections.Generic;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.Queries
{
    public class FetchContentPagesByIdsQuery : IQuery
    {
        /// <summary>
        /// The ids of the content pages requested
        /// </summary>
        public IList<string> Ids { get; set; }
    }
}