using LightOps.Commerce.Proto.Services.ContentPage;
using LightOps.CQRS.Api.Queries;

namespace LightOps.Commerce.Services.ContentPage.Api.Queries
{
    public class FetchContentPagesBySearchQuery : IQuery
    {
        /// <summary>
        /// The term to search for, if any
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// Search only in localized strings with a specific language code, if any specified.
        /// ISO 639 2-letter language code matched with ISO 3166 2-letter country code, eg. en-US, da-DK
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Search only in children with a specific parent id, if any specified. For no parent: 'gid://'
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// The page cursor to use
        /// </summary>
        public string PageCursor { get; set; }

        /// <summary>
        /// The page size to use
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Sort the underlying list by the given key
        /// </summary>
        public GetBySearchRequest.Types.SortKey SortKey { get; set; }

        /// <summary>
        /// Whether to reverse the order of the underlying list
        /// </summary>
        public bool Reverse { get; set; }
    }
}