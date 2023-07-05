using MelonBookshelfApi.ResponceModels;

namespace MelonBookshelfApi.Services.Contracts
{
    public interface IResourceService
    {
        Task<IEnumerable<SearchResult>> SearchResources(string type, string category, string title);
    }
}
