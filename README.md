# LightOps Commerce - Content Page Service

Microservice for content pages.

Defines content pages.  
Uses CQRS to fetch entities from data-source without defining any.  
Provides gRPC services for integrations into other services.

![Nuget](https://img.shields.io/nuget/v/LightOps.Commerce.Services.ContentPage)

| Branch | CI |
| --- | --- |
| master | ![Build Status](https://dev.azure.com/sorendev/LightOps%20Commerce/_apis/build/status/SorenA.lightops-commerce-services-content-page?branchName=master) |
| develop | ![Build Status](https://dev.azure.com/sorendev/LightOps%20Commerce/_apis/build/status/SorenA.lightops-commerce-services-content-page?branchName=develop) |

## gRPC services

Protobuf service definitions located at [SorenA/lightops-commerce-proto](https://github.com/SorenA/lightops-commerce-proto).

Content Page v1 is implemented in `Domain.Services.V1.ContentPageGrpcService`.

Health v1 is implemented in `Domain.Services.V1.HealthGrpcService`.

### Health-check

Health-checks conforms to the [GRPC Health Checking Protocol](https://github.com/grpc/grpc/blob/master/doc/health-checking.md)

Available services are as follows

```bash
service = '' - System as a whole
service = 'service.content_page.v1.ProtoContentPageService' - ContentPage v1
```

For embedding a gRPC client for use with Kubernetes, see [grpc-ecosystem/grpc-health-probe](https://github.com/grpc-ecosystem/grpc-health-probe)

## Samples

A sample application hosting the gRPC service with mock data is available in the `samples/Sample.ContentPageService` project.

## Requirements

LightOps packages available on NuGet:

- `LightOps.DependencyInjection`
- `LightOps.CQRS`
- `LightOps.Mapping`

## Using the service component

Register during startup through the `AddContentPageService(options)` extension on `IDependencyInjectionRootComponent`.

```csharp
services.AddLightOpsDependencyInjection(root =>
{
    root
        .AddMapping()
        .AddCqrs()
        .AddContentPageService(service =>
        {
            // Configure service
            // ...
        });
});

services.AddGrpc();
```

Register gRPC services for integrations.

```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ContentPageGrpcService>();
    endpoints.MapGrpcService<HealthGrpcService>();

    // Map other endpoints...
});
```

### Configuration options

A component backend is required, defining the query handlers tied to a data-source, see **Query handlers** section bellow for more.

A custom backend, or one of the following standard backends can be used:

- InMemory

### Overridables

Using the `IContentPageServiceComponent` configuration, the following can be overridden:

```csharp
public interface IContentPageServiceComponent
{
    #region Services
    IContentPageServiceComponent OverrideHealthService<T>() where T : IHealthService;
    IContentPageServiceComponent OverrideContentPageService<T>() where T : IContentPageService;
    #endregion Services

    #region Mappers
    IContentPageServiceComponent OverrideProtoContentPageMapperV1<T>() where T : IMapper<IContentPage, Proto.Services.ContentPage.V1.ProtoContentPage>;
    #endregion Mappers

    #region Query Handlers
    IContentPageServiceComponent OverrideCheckContentPageHealthQueryHandler<T>() where T : ICheckContentPageHealthQueryHandler;
    IContentPageServiceComponent OverrideFetchContentPagesByParentIdQueryHandler<T>() where T : IFetchContentPagesByParentIdQueryHandler;
    IContentPageServiceComponent OverrideFetchContentPagesByRootQueryHandler<T>() where T : IFetchContentPagesByRootQueryHandler;
    IContentPageServiceComponent OverrideFetchContentPagesBySearchQueryHandler<T>() where T : IFetchContentPagesBySearchQueryHandler;
    IContentPageServiceComponent OverrideFetchContentPageByHandleQueryHandler<T>() where T : IFetchContentPageByHandleQueryHandler;
    IContentPageServiceComponent OverrideFetchContentPageByIdQueryHandler<T>() where T : IFetchContentPageByIdQueryHandler;
    #endregion Query Handlers
}
```

`IContentPageService` is used by the gRPC services and query the data using the `IQueryDispatcher` from the `LightOps.CQRS` package.

The mappers are used for mapping the internal data structure to the versioned protobuf messages.

## Backend modules

### In-Memory

Register during startup through the `UseInMemoryBackend(root, options)` extension on `IContentPageServiceComponent`.

```csharp
root.AddContentPageService(service =>
{
    service.UseInMemoryBackend(root, backend =>
    {
        var contentPages = new List<IContentPage>();
        // ...

        backend.UseContentPages(contentPages);
    });

    // Configure service
    // ...
});
```
