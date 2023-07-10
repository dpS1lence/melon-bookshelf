namespace MelonBookshelfApi.Services.Contracts
{
    public interface IBaseUserAutomationService
    {
        Task ReturnPhysicalResource(int resourceId, string userId);
        
        Task UpvoteRequest(int requestId, string userId);
        
        Task FollowRequest(int requestId, string userId);

        Task GetPhysicalResource(int resourceId, string userId);
    }
}
