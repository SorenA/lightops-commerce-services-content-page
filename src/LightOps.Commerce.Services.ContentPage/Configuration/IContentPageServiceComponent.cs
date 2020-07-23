using LightOps.Commerce.Proto.Types;
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
        IContentPageServiceComponent OverrideContentPageProtoMapper<T>() where T : IMapper<IContentPage, ContentPageProto>;
        IContentPageServiceComponent OverrideImageProtoMapper<T>() where T : IMapper<IImage, ImageProto>;
        #endregion Mappers

        #region Query Handlers
        IContentPageServiceComponent OverrideCheckContentPageHealthQueryHandler<T>() where T : ICheckContentPageHealthQueryHandler;

        IContentPageServiceComponent OverrideFetchContentPagesByHandlesQueryHandler<T>() where T : IFetchContentPagesByHandlesQueryHandler;
        IContentPageServiceComponent OverrideFetchContentPagesByIdsQueryHandler<T>() where T : IFetchContentPagesByIdsQueryHandler;
        IContentPageServiceComponent OverrideFetchContentPagesBySearchQueryHandler<T>() where T : IFetchContentPagesBySearchQueryHandler;
        #endregion Query Handlers
    }
}