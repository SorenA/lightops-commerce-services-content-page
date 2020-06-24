using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.Queries
{
    public class FetchContentPagesBySearchQuery : IQuery
    {
        public string SearchTerm { get; set; }
    }
}