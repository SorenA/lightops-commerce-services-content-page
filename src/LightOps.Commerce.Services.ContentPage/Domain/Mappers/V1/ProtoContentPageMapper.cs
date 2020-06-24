using System.Linq;
using LightOps.Commerce.Proto.Services.ContentPage.V1;
using LightOps.Commerce.Services.ContentPage.Api.Models;
using LightOps.Mapping.Api.Mappers;
using LightOps.Mapping.Api.Services;

// ReSharper disable UseObjectOrCollectionInitializer

namespace LightOps.Commerce.Services.ContentPage.Domain.Mappers.V1
{
    public class ProtoContentPageMapper : IMapper<IContentPage, ProtoContentPage>
    {
        private readonly IMappingService _mappingService;

        public ProtoContentPageMapper(IMappingService mappingService)
        {
            _mappingService = mappingService;
        }

        public ProtoContentPage Map(IContentPage source)
        {
            var dest = new ProtoContentPage();

            dest.Id = source.Id;
            dest.Handle = source.Handle;
            dest.Url = source.Url;

            dest.ParentContentPageId = source.ParentContentPageId;

            dest.Title = source.Title;
            dest.Type = source.Type;
            dest.Url = source.Description;

            dest.SeoTitle = source.SeoTitle;
            dest.SeoDescription = source.SeoDescription;

            dest.PrimaryImage = source.PrimaryImage;

            return dest;
        }
    }
}
