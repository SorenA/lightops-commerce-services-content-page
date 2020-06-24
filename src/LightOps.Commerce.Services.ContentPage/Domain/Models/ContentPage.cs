using System.Collections.Generic;
using LightOps.Commerce.Services.ContentPage.Api.Models;

namespace LightOps.Commerce.Services.ContentPage.Domain.Models
{
    public class ContentPage : IContentPage
    {
        public string Id { get; set; }
        public string Handle { get; set; }
        public string Url { get; set; }

        public string ParentContentPageId { get; set; }

        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }

        public string PrimaryImage { get; set; }
    }
}