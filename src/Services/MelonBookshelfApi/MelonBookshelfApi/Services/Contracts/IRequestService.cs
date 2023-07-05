using MelonBookshelfApi.CustomObjects.Enums;
using MelonBookshelfApi.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Services.Contracts
{
    public interface IRequestService
    {
        Task<ProcessRequestResult> ProcessRequestAsync(ResourceRequestDto requestDto, string userId);
    }
}
