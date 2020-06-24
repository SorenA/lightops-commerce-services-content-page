using System;
using LightOps.DependencyInjection.Configuration;

namespace LightOps.Commerce.Services.ContentPage.Configuration
{
    public static class DependencyInjectionRootComponentExtensions
    {

        public static IDependencyInjectionRootComponent AddContentPageService(this IDependencyInjectionRootComponent rootComponent, Action<IContentPageServiceComponent> componentConfig = null)
        {
            var component = new ContentPageServiceComponent();

            // Invoke config delegate
            componentConfig?.Invoke(component);

            // Attach to root component
            rootComponent.AttachComponent(component);

            return rootComponent;
        }
    }
}
