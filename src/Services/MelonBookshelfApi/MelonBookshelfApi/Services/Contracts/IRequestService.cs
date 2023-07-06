using MelonBookshelfApi.CustomObjects.Enums;
using MelonBookshelfApi.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Services.Contracts
{
    public interface IRequestService
    {
        Task<IEnumerable<ResourceRequestDto>> GetRequestsByUserId(string userId);
        Task<ProcessRequestResult> ProcessRequestAsync(ResourceRequestDto requestDto, string userId);
    }
}
