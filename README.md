# LightOps Commerce - Content Page Service

Microservice for content pages.

Defines content pages.  
Uses CQRS to fetch entities from data-source without defining any.  
Provides gRPC services for integrations into other services.

![Nuget](https://img.shields.io/nuget/v/LightOps.Commerce.Services.ContentPage)

| Branch | CI |
| --- | --- |
| master | ![Build Status](https://dev.azure.com/sorendev/LightOps%20Commerce/_apis/build/status/LightOps.Commerce.Services.ContentPage?branchName=master) |
| develop | ![Build Status](https://dev.azure.com/sorendev/LightOps%20Commerce/_apis/build/status/LightOps.Commerce.Services.ContentPage?branchName=develop) |

## gRPC services

Protobuf service definitions located at [SorenA/lightops-commerce-proto](https://github.com/SorenA/lightops-commerce-proto).

Content Page is implemented in `Domain.GrpcServices.ContentPageGrpcService`.

Health is implemented in `Domain.GrpcServices.HealthGrpcService`.

### Health-check

Health-checks conforms to the [GRPC Health Checking Protocol](https://github.com/grpc/grpc/blob/master/doc/health-checking.md)

Available services are as follows

```bash
service = '' - System as a whole
service = 'lightops.service.ContentPageService' - ContentPage
```

For embedding a gRPC client for use with Kubernetes, see [grpc-ecosystem/grpc-health-probe](https://github.com/grpc-ecosystem/grpc-health-probe)

## Samples

A sample application hosting the gRPC service with mock data is available in the `samples/Sample.ContentPageService` project.

## Requirements

LightOps packages available on NuGet:

- `LightOps.DependencyInjection`
- `LightOps.CQRS`

## Using the service component

Register during startup through the `AddContentPageService(options)` extension on `IDependencyInjectionRootComponent`.

```csharp
services.AddLightOpsDependencyInjection(root =>
{
    root
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

The gRPC services use `ICommandDispatcher` & `IQueryDispatcher` from the `LightOps.CQRS` package to dispatch commands and queries, see configuration bellow.

### Configuration options

A component backend is required, implementing the command & query handlers tied to a data-source, see configuration overridables bellow.

A custom backend, or one of the following standard backends can be used:

- InMemory

### Overridables

Using the `IContentPageServiceComponent` configuration, the following can be overridden:

```csharp
public interface IContentPageServiceComponent
{
    #region Query Handlers
    IContentPageServiceComponent OverrideCheckContentPageHealthQueryHandler<T>() where T : ICheckContentPageHealthQueryHandler;

    IContentPageServiceComponent OverrideFetchContentPagesByHandlesQueryHandler<T>() where T : IFetchContentPagesByHandlesQueryHandler;
    IContentPageServiceComponent OverrideFetchContentPagesByIdsQueryHandler<T>() where T : IFetchContentPagesByIdsQueryHandler;
    IContentPageServiceComponent OverrideFetchContentPagesBySearchQueryHandler<T>() where T : IFetchContentPagesBySearchQueryHandler;
    #endregion Query Handlers

    #region Command Handlers
    IContentPageServiceComponent OverridePersistContentPageCommandHandler<T>() where T : IPersistContentPageCommandHandler;
    IContentPageServiceComponent OverrideDeleteContentPageCommandHandler<T>() where T : IDeleteContentPageCommandHandler;
    #endregion Command Handlers
}
```


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
