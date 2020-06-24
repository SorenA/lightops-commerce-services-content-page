using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.Commerce.Services.ContentPage.Api.Services;
using LightOps.CQRS.Api.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.ContentPage.Domain.Services
{
    public class HealthService : IHealthService
    {
        private readonly IQueryDispatcher _queryDispatcher;

        public HealthService(IQueryDispatcher queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        public Task<HealthStatus> CheckContentPage()
        {
            return _queryDispatcher.DispatchAsync<CheckContentPageHealthQuery, HealthStatus>(new CheckContentPageHealthQuery());
        }
    }
}