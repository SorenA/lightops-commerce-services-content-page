using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LightOps.Commerce.Services.ContentPage.Api.Services
{
    public interface IHealthService
    {
        Task<HealthStatus> CheckContentPage();
    }
}