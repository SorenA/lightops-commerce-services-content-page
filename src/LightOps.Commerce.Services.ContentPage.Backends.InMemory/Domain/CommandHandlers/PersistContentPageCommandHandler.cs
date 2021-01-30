using System;
using System.Linq;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.CommandHandlers;
using LightOps.Commerce.Services.ContentPage.Api.Commands;
using LightOps.Commerce.Services.ContentPage.Backends.InMemory.Api.Providers;

namespace LightOps.Commerce.Services.ContentPage.Backends.InMemory.Domain.CommandHandlers
{
    public class PersistContentPageCommandHandler : IPersistContentPageCommandHandler
    {
        private readonly IInMemoryContentPageProvider _inMemoryContentPageProvider;

        public PersistContentPageCommandHandler(IInMemoryContentPageProvider inMemoryContentPageProvider)
        {
            _inMemoryContentPageProvider = inMemoryContentPageProvider;
        }

        public Task HandleAsync(PersistContentPageCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Id))
            {
                throw new ArgumentException("ID missing.");
            }

            if (command.ContentPage.Id != command.Id)
            {
                throw new ArgumentException("Provided ID and entity ID do not match.");
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

            // Add entity to provider
            _inMemoryContentPageProvider.ContentPages?.Add(command.ContentPage);

            return Task.CompletedTask;
        }
    }
}