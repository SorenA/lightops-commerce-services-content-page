using System.Threading.Tasks;
using Grpc.Core;
using LightOps.Commerce.Proto.Services.ContentPage.V1;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Commerce.Services.ContentPage.Api.Services;
using LightOps.Mapping.Api.Services;
using Microsoft.Extensions.Logging;

namespace LightOps.Commerce.Services.ContentPage.Domain.Services.V1
{
    public class ContentPageGrpcService : ProtoContentPageService.ProtoContentPageServiceBase
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

        public override async Task<ProtoGetContentPageResponse> GetContentPage(ProtoGetContentPageRequest request, ServerCallContext context)
        {
            IContentPage entity;
            switch (request.IdentifierCase)
            {
                case ProtoGetContentPageRequest.IdentifierOneofCase.Id:
                    entity = await _contentPageService.GetByIdAsync(request.Id);
                    break;
                case ProtoGetContentPageRequest.IdentifierOneofCase.Handle:
                    entity = await _contentPageService.GetByHandleAsync(request.Handle);
                    break;
                default:
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Missing identifier"));
            }

            var protoEntity = _mappingService.Map<IContentPage, ProtoContentPage>(entity);

            var result = new ProtoGetContentPageResponse
            {
                ContentPage = protoEntity,
            };

            return result;
        }

        public override async Task<GetContentPagesByIdsResponse> GetContentPagesByIds(GetContentPagesByIdsRequest request, ServerCallContext context)
        {
            var entities = await _contentPageService.GetByIdAsync(request.Ids);
            var protoEntities = _mappingService.Map<IContentPage, ProtoContentPage>(entities);

            var result = new GetContentPagesByIdsResponse();
            result.ContentPages.AddRange(protoEntities);

            return result;
        }

        public override async Task<GetContentPagesByHandlesResponse> GetContentPagesByHandles(GetContentPagesByHandlesRequest request, ServerCallContext context)
        {
            var entities = await _contentPageService.GetByHandleAsync(request.Handles);
            var protoEntities = _mappingService.Map<IContentPage, ProtoContentPage>(entities);

            var result = new GetContentPagesByHandlesResponse();
            result.ContentPages.AddRange(protoEntities);

            return result;
        }

        public override async Task<ProtoGetContentPagesByParentIdResponse> GetContentPagesByParentId(ProtoGetContentPagesByParentIdRequest request, ServerCallContext context)
        {
            var entities = await _contentPageService.GetByParentIdAsync(request.ParentId);
            var protoEntities = _mappingService.Map<IContentPage, ProtoContentPage>(entities);

            var result = new ProtoGetContentPagesByParentIdResponse();
            result.ContentPages.AddRange(protoEntities);

            return result;
        }

        public override async Task<ProtoGetContentPagesByParentIdsResponse> GetContentPagesByParentIds(ProtoGetContentPagesByParentIdsRequest request, ServerCallContext context)
        {
            var entities = await _contentPageService.GetByParentIdAsync(request.ParentIds);
            var protoEntities = _mappingService.Map<IContentPage, ProtoContentPage>(entities);

            var result = new ProtoGetContentPagesByParentIdsResponse();
            result.ContentPages.AddRange(protoEntities);

            return result;
        }

        public override async Task<ProtoGetContentPagesByRootResponse> GetContentPagesByRoot(ProtoGetContentPagesByRootRequest request, ServerCallContext context)
        {
            var entities = await _contentPageService.GetByRootAsync();
            var protoEntities = _mappingService.Map<IContentPage, ProtoContentPage>(entities);

            var result = new ProtoGetContentPagesByRootResponse();
            result.ContentPages.AddRange(protoEntities);

            return result;
        }

        public override async Task<ProtoGetContentPagesBySearchResponse> GetContentPagesBySearch(ProtoGetContentPagesBySearchRequest request, ServerCallContext context)
        {
            var entities = await _contentPageService.GetBySearchAsync(request.SearchTerm);
            var protoEntities = _mappingService.Map<IContentPage, ProtoContentPage>(entities);

            var result = new ProtoGetContentPagesBySearchResponse();
            result.ContentPages.AddRange(protoEntities);

            return result;
        }
    }
}
