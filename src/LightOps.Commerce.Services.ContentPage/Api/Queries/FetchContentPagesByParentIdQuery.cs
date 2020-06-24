using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.Queries
{
    public class FetchContentPagesByParentIdQuery : IQuery
    {
        public string ParentId { get; set; }
    }
}