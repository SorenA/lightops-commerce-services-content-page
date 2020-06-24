using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.Queries
{
    public class FetchContentPageByIdQuery : IQuery
    {
        public string Id { get; set; }
    }
}