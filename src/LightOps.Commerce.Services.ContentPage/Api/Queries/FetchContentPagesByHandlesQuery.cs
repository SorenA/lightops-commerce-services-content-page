using System.Collections.Generic;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.Queries
{
    public class FetchContentPagesByHandlesQuery : IQuery
    {
        /// <summary>
        /// The handles of the content page requested
        /// </summary>
        public IList<string> Handles { get; set; }
    }
}