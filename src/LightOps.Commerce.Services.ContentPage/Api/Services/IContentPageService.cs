using System.Collections.Generic;
using System.Threading.Tasks;
using LightOps.Commerce.Services.ContentPage.Api.Models;

namespace LightOps.Commerce.Services.ContentPage.Api.Services
{
    public interface IContentPageService
    {
        Task<IContentPage> GetByIdAsync(string id);
        Task<IList<IContentPage>> GetByIdAsync(IList<string> ids);

        Task<IContentPage> GetByHandleAsync(string handle);
        Task<IList<IContentPage>> GetByHandleAsync(IList<string> handles);

        Task<IList<IContentPage>> GetByParentIdAsync(string parentId);

        Task<IList<IContentPage>> GetByRootAsync();
        Task<IList<IContentPage>> GetBySearchAsync(string searchTerm);
    }
}