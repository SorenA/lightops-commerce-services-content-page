using Google.Protobuf.WellKnownTypes;
using LightOps.Commerce.Proto.Types;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Mapping.Api.Mappers;
using LightOps.Mapping.Api.Services;

namespace LightOps.Commerce.Services.ContentPage.Domain.Mappers
{
    public class ContentPageProtoMapper : IMapper<IContentPage, ContentPageProto>
    {
        private readonly IMappingService _mappingService;

        public ContentPageProtoMapper(IMappingService mappingService)
        {
            _mappingService = mappingService;
        }

        public ContentPageProto Map(IContentPage src)
        {
            return new ContentPageProto
            {
                Id = src.Id,
                ParentId = src.ParentId,
                Handle = src.Handle,
                Title = src.Title,
                Url = src.Url,
                Type = src.Type,
                Summary = src.Summary,
                CreatedAt = Timestamp.FromDateTime(src.CreatedAt.ToUniversalTime()),
                UpdatedAt = Timestamp.FromDateTime(src.UpdatedAt.ToUniversalTime()),
                PrimaryImage = _mappingService.Map<IImage, ImageProto>(src.PrimaryImage),
            };
        }
    }
}
