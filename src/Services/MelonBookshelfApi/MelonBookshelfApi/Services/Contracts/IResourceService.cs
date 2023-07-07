using MelonBookshelfApi.ResponceModels;

namespace MelonBookshelfApi.Services.Contracts
{
    public interface IResourceService
    {
        IEnumerable<ResourceHRModel> GetAllResourcesHR();
        IEnumerable<ResourceModel> GetAllResources();
        Task AddResource(ResourceHRModel model);
        IEnumerable<string> GetCategoriesAsync();
        Task<PhysicalResource> GetPhysicalResourceByIdAsync(string pysicalResourceId);
        Task<IEnumerable<PhysicalResourceTaken>> GetPhysicalResourcesByUserIdAsync(string userId);
        Task<IEnumerable<ResourceModel>> SearchResources(string? type, string? category, string? title);
        Task<ResourceModel> GetResourceByIdAsync(string resourceId);
    }
}
