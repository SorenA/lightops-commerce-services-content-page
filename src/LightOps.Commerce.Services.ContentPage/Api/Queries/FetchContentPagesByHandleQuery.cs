﻿using System.Collections.Generic;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.Queries
{
    public class FetchContentPagesByHandleQuery : IQuery
    {
        public IList<string> Handles { get; set; }
    }
}