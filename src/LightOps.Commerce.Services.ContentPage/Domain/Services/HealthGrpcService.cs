﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using LightOps.Commerce.Proto.Grpc.Health;
using LightOps.Commerce.Services.ContentPage.Api.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace LightOps.Commerce.Services.ContentPage.Domain.Services
{
    public class HealthGrpcService : Health.HealthBase
    {
        private readonly ILogger<HealthGrpcService> _logger;
        private readonly IHealthService _healthService;

        public HealthGrpcService(
            ILogger<HealthGrpcService> logger,
            IHealthService healthService)
        {
            _logger = logger;
            _healthService = healthService;
        }

        public override async Task<HealthCheckResponse> Check(HealthCheckRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.Service))
            {
                // Perform overall health-check
                var statusMap = new Dictionary<string, HealthCheckResponse.Types.ServingStatus>();

                // Check all services
                statusMap.Add("service.content_page.v1.ProtoContentPageService", await GetContentPageServiceStatusAsync());

                return new HealthCheckResponse
                {
                    Status = statusMap.All(x => x.Value == HealthCheckResponse.Types.ServingStatus.Serving)
                        ? HealthCheckResponse.Types.ServingStatus.Serving
                        : HealthCheckResponse.Types.ServingStatus.NotServing,
                };
            }

            var servingStatus = HealthCheckResponse.Types.ServingStatus.Unknown;
            switch (request.Service)
            {
                case "service.content_page.v1.ProtoContentPageService":
                    servingStatus = await GetContentPageServiceStatusAsync();
                    break;
                default:
                    throw new RpcException(new Status(StatusCode.NotFound, $"Service {request.Service} not found."));
            }

            return new HealthCheckResponse
            {
                Status = servingStatus,
            };
        }

        private async Task<HealthCheckResponse.Types.ServingStatus> GetContentPageServiceStatusAsync()
        {
            return await _healthService.CheckContentPage() == HealthStatus.Healthy
                ? HealthCheckResponse.Types.ServingStatus.Serving
                : HealthCheckResponse.Types.ServingStatus.NotServing;
        }
    }
}