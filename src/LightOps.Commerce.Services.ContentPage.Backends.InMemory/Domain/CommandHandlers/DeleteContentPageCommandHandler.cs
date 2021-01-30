using System;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.CommandHandlers;
using LightOps.Commerce.Services.ContentPage.Api.Commands;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.CommandHandlers
{
    public class DeleteContentPageCommandHandler : IDeleteContentPageCommandHandler
    {
        private readonly IInMemoryContentPageProvider _inMemoryContentPageProvider;

        public DeleteContentPageCommandHandler(IInMemoryContentPageProvider inMemoryContentPageProvider)
        {
            _inMemoryContentPageProvider = inMemoryContentPageProvider;
        }

        public Task HandleAsync(DeleteContentPageCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Id))
            {
                throw new ArgumentException("ID missing.");
            }

            // Check if entity already exists
            var entity = _inMemoryContentPageProvider
                .ContentPages?
                .FirstOrDefault(x => x.Id == command.Id);

            // Delete old if found
            if (entity != null)
            {
                _inMemoryContentPageProvider.ContentPages?.Remove(entity);
            }

            return Task.CompletedTask;
        }
    }
}