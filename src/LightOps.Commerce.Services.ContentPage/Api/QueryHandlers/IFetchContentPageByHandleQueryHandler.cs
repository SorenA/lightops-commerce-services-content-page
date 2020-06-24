﻿using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Queries;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.QueryHandlers
{
    public interface IFetchContentPageByHandleQueryHandler : IQueryHandler<FetchContentPageByHandleQuery, IContentPage>
    {

    }
}