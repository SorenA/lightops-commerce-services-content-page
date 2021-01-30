using LightOps.CQRS.Api.Commands;

namespace LightOps.Commerce.Services.ContentPage.Api.Commands
{
    public class DeleteContentPageCommand : ICommand
    {
        /// <summary>
        /// The id of the content page to delete
        /// </summary>
        public string Id { get; set; }
    }
}