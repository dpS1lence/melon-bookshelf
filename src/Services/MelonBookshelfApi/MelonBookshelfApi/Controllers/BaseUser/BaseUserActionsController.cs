using MelonBookchelfApi.Infrastructure.Data.Models;
using MelonBookshelfApi.RequestDtos;
using MelonBookshelfApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MelonBookshelfApi.Controllers.BaseUser
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class BaseUserActionsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IBaseUserAutomationService _automationService;

        public BaseUserActionsController(ILogger<BaseUserActionsController> logger, IBaseUserAutomationService automationService)
        {
            _logger = logger;
            _automationService = automationService;
        }

        [HttpPut]
        [Route("user-actions/{resourceId}/{userId}/return-physical-resource")]
        public async Task<IActionResult> ReturnPhysicalResource(int resourceId, string userId)
        {
            await _automationService.ReturnPhysicalResource(resourceId, userId);

            return Ok();
        }

        [HttpPut]
        [Route("user-actions/{resourceId}/{userId}/get-physical-resource")]
        public async Task<IActionResult> GetPhysicalResource(int resourceId, string userId)
        {
            await _automationService.GetPhysicalResource(resourceId, userId);

            return Ok();
        }

        [HttpPut]
        [Route("user-actions/{requestId}/{userId}/upvote")]
        public async Task<IActionResult> UpvoteRequest(int requestId, string userId)
        {
            await _automationService.UpvoteRequest(requestId, userId);

            return Ok();
        }

        [HttpPut]
        [Route("user-actions/{requestId}/{userId}/follow")]
        public async Task<IActionResult> FollowRequest(int requestId, string userId)
        {
            await _automationService.FollowRequest(requestId, userId);

            return Ok();
        }
    }
}
