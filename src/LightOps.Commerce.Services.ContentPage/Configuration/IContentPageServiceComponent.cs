using LightOps.Commerce.Services.ContentPage.Api.CommandHandlers;
using LightOps.Commerce.Services.ContentPage.Api.QueryHandlers;

namespace LightOps.Commerce.Services.ContentPage.Configuration
{
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
}