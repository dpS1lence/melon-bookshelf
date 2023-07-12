using MelonBookshelfApi.ResponceModels;

namespace MelonBookshelfApi.Services.Contracts
{
    public interface IResourceService
    {
        Task<IEnumerable<ResourceModel>> GetAllResourcesHR();
        Task<IEnumerable<ResourceModel>> GetAllResources();
        Task AddResource(ResourceModel model);
        IEnumerable<string> GetCategoriesAsync();
        Task<ResourceModel> GetPhysicalResourceByIdAsync(string pysicalResourceId);
        Task<IEnumerable<ResourceModel>> GetPhysicalResourcesByUserIdAsync(string userId);
        Task<IEnumerable<ResourceModel>> SearchResources(string? type, string? category, string? title);
        Task<ResourceModel> GetResourceByIdAsync(string resourceId);
    }
}
