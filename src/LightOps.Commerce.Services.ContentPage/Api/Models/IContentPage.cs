namespace LightOps.Commerce.Services.ContentPage.Api.Models
{
    public interface IContentPage
    {
        public string Id { get; set; }
        public string Handle { get; set; }
        public string Url { get; set; }

        string ParentContentPageId { get; set; }

        string Title { get; set; }
        string Type { get; set; }
        string Description { get; set; }

        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }

        string PrimaryImage { get; set; }
    }
}