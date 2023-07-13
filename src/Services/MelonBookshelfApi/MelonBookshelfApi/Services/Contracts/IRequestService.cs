using MelonBookshelfApi.CustomObjects;
using MelonBookshelfApi.RequestDtos;
using MelonBookshelfApi.ResponceModels;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Services.Contracts
{
    public interface IRequestService
    {
        Task<IEnumerable<UserRequestedResourceModel>> GetRequests();
        Task<ResourceRequestDto> GetRequestById(int requestId);
        Task<IEnumerable<UserRequestedResourceModel>> GetRequestsByUserId(string userId);
        Task<ProcessRequestResult> ProcessRequestAsync(ResourceRequestDto requestDto, string userId);
    }
}
