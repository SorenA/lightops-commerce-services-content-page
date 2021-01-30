using LightOps.Commerce.Services.ContentPage.Api.Commands;
using LightOps.CQRS.Api.Commands;

namespace LightOps.Commerce.Services.ContentPage.Api.CommandHandlers
{
    public interface IPersistContentPageCommandHandler : ICommandHandler<PersistContentPageCommand>
    {
        
    }
}