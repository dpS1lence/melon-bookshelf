using MelonBookshelfApi.CustomObjects.Enums;
using MelonBookshelfApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Controllers.HR
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class HRActionsController : Controller
    {
        private readonly IHrActionsService _automationService;

        public HRActionsController(IHrActionsService automationService)
        {
            _automationService = automationService;
        }

        [HttpPut("hr-actions/requests/{requestId}/confirm")]
        public async Task<IActionResult> ConfirmRequest(int requestId)
        {
            await _automationService.Confirm(requestId);

            return Ok();
        }

        [HttpPut("hr-actions/requests/{requestId}/reject")]
        public async Task<IActionResult> RejectRequest(int requestId, [FromBody] string justification)
        {
            await _automationService.Reject(requestId, justification);

            return Ok();
        }

        [HttpPut("hr-actions/resources/{resourceId}/inprogress")]
        public async Task<IActionResult> SetResourceInProgress(int resourceId)
        {
            await _automationService.SetResource(resourceId, ResourceStatus.Processing.ToString());

            return Ok();
        }

        [HttpPut("hr-actions/resources/{resourceId}/delivery")]
        public async Task<IActionResult> SetResourceInDelivery(int resourceId)
        {
            await _automationService.SetResource(resourceId, ResourceStatus.InDelivery.ToString());

            return Ok();
        }

        [HttpPut("hr-actions/resources/{resourceId}/delivered")]
        public async Task<IActionResult> SetResourceDelivered(int resourceId)
        {
            await _automationService.SetResource(resourceId, ResourceStatus.Delivered.ToString());

            return Ok();
        }
    }
}
