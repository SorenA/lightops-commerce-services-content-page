using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;
using LightOps.Commerce.Services.ContentPage.Api.Services;
using LightOps.Mapping.Api.Mappers;

namespace LightOps.Commerce.Services.ContentPage.Configuration
{
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
        IContentPageServiceComponent OverrideFetchContentPagesByHandlesQueryHandler<T>() where T : IFetchContentPagesByHandlesQueryHandler;
        IContentPageServiceComponent OverrideFetchContentPagesByIdsQueryHandler<T>() where T : IFetchContentPagesByIdsQueryHandler;
        IContentPageServiceComponent OverrideFetchContentPageByHandleQueryHandler<T>() where T : IFetchContentPageByHandleQueryHandler;
        IContentPageServiceComponent OverrideFetchContentPageByIdQueryHandler<T>() where T : IFetchContentPageByIdQueryHandler;
        #endregion Query Handlers
    }
}