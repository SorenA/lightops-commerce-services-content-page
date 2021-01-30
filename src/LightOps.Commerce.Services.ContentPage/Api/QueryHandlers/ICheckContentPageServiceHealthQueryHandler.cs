using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.CQRS.Api.Queries;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.ContentPage.Api.QueryHandlers
{
    public interface ICheckContentPageServiceHealthQueryHandler : IQueryHandler<CheckContentPageServiceHealthQuery, HealthStatus>
    {

    }
}