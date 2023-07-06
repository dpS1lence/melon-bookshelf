using MelonBookshelfApi.ResponceModels;

namespace MelonBookshelfApi.Services.Contracts
{
    public interface IResourceService
    {
        IEnumerable<string> GetCategoriesAsync();
        Task<PhysicalResource> GetPhysicalResourceByIdAsync(string pysicalResourceId);
        Task<IEnumerable<PhysicalResourceTaken>> GetPhysicalResourcesByUserIdAsync(string userId);
        Task<IEnumerable<SearchResult>> SearchResources(string? type, string? category, string? title);
    }
}
