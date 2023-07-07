namespace MelonBookshelfApi.Services.Contracts
{
    public interface IHrActionsService
    {
        Task Confirm(int requestId);
        Task Reject(int requestId, string justification);
        Task SetResource(int resourceId, string processing);
    }
}
