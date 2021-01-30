using LightOps.CQRS.Api.Commands;

namespace LightOps.Commerce.Services.ContentPage.Api.Commands
{
    public class PersistContentPageCommand : ICommand
    {
        /// <summary>
        /// The id of the content page to persist
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The content page to persist
        /// </summary>
        public Proto.Types.ContentPage ContentPage { get; set; }
    }
}