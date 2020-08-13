using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using LightOps.Commerce.Proto.Services;
using LightOps.Commerce.Proto.Types;
using LightOps.Commerce.Services.ContentPage.Api.Enums;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Services;
using LightOps.Mapping.Api.Services;
using Microsoft.Extensions.Logging;

namespace LightOps.Commerce.Services.ContentPage.Domain.Services.Grpc
{
    public class ContentPageGrpcService : ContentPageProtoService.ContentPageProtoServiceBase
    {
        private readonly ILogger<ContentPageGrpcService> _logger;
        private readonly IContentPageService _contentPageService;
        private readonly IMappingService _mappingService;

        public ContentPageGrpcService(
            ILogger<ContentPageGrpcService> logger,
            IContentPageService contentPageService,
            IMappingService mappingService)
        {
            _logger = logger;
            _contentPageService = contentPageService;
            _mappingService = mappingService;
        }

        public override async Task<GetContentPagesByHandlesProtoResponse> GetContentPagesByHandles(GetContentPagesByHandlesProtoRequest request, ServerCallContext context)
        {
            var entities = await _contentPageService.GetByHandleAsync(request.Handles);
            var protoEntities = _mappingService.Map<IContentPage, ContentPageProto>(entities);

            var result = new GetContentPagesByHandlesProtoResponse();
            result.ContentPages.AddRange(protoEntities);

            return result;
        }

        public override async Task<GetContentPagesByIdsProtoResponse> GetContentPagesByIds(GetContentPagesByIdsProtoRequest request, ServerCallContext context)
        {
            var entities = await _contentPageService.GetByIdAsync(request.Ids);
            var protoEntities = _mappingService.Map<IContentPage, ContentPageProto>(entities);

            var result = new GetContentPagesByIdsProtoResponse();
            result.ContentPages.AddRange(protoEntities);

            return result;
        }

        public override async Task<GetContentPagesBySearchProtoResponse> GetContentPagesBySearch(GetContentPagesBySearchProtoRequest request, ServerCallContext context)
        {
            var searchResult = await _contentPageService.GetBySearchAsync(
                request.SearchTerm,
                request.ParentId,
                request.PageCursor,
                request.PageSize ?? 24,
                ConvertSortKey(request.SortKey),
                request.Reverse ?? false);

            // Map results
            var protoEntities = searchResult
                .Results
                .Select(x => new GetContentPagesBySearchProtoResponse.Types.ContentPageResult
                {
                    Cursor = x.Cursor,
                    Node = _mappingService.Map<IContentPage, ContentPageProto>(x.Node),
                })
                .ToList();

            var result = new GetContentPagesBySearchProtoResponse
            {
                HasNextPage = searchResult.HasNextPage,
                HasPreviousPage = searchResult.HasPreviousPage,
                StartCursor = searchResult.StartCursor ?? string.Empty,
                EndCursor = searchResult.EndCursor ?? string.Empty,
                TotalResults = searchResult.TotalResults,
            };
            result.Results.AddRange(protoEntities);

            return result;
        }

        private ContentPageSortKey ConvertSortKey(ContentPageSortKeyProto sortKey)
        {
            switch (sortKey)
            {
                case ContentPageSortKeyProto.Default:
                    return ContentPageSortKey.Default;
                case ContentPageSortKeyProto.Title:
                    return ContentPageSortKey.Title;
                case ContentPageSortKeyProto.CreatedAt:
                    return ContentPageSortKey.CreatedAt;
                case ContentPageSortKeyProto.UpdatedAt:
                    return ContentPageSortKey.UpdatedAt;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
