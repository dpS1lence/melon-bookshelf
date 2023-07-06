using MelonBookshelfApi.ResponceModels;

namespace MelonBookshelfApi.Services.Contracts
{
    public interface IResourceService
    {
        IEnumerable<ResourceModel> GetAllResources();
        IEnumerable<string> GetCategoriesAsync();
        Task<PhysicalResource> GetPhysicalResourceByIdAsync(string pysicalResourceId);
        Task<IEnumerable<PhysicalResourceTaken>> GetPhysicalResourcesByUserIdAsync(string userId);
        Task<IEnumerable<ResourceModel>> SearchResources(string? type, string? category, string? title);
    }
}
