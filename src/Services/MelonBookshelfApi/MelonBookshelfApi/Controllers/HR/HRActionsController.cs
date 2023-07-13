using MelonBookshelfApi.CustomObjects.Enums;
using MelonBookshelfApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Controllers.HR
{
    [ApiController]
    [Route("api")]
    [Authorize(Roles = "Admin")]
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

        [HttpPut("hr-actions/requests/{requestId}/inprogress")]
        public async Task<IActionResult> SetRequestInProgress(int requestId)
        {
            await _automationService.SetRequestDeliveryStatus(requestId, ResourceStatus.Processing.ToString());

            return Ok();
        }

        [HttpPut("hr-actions/requests/{requestId}/delivery")]
        public async Task<IActionResult> SetRequestInDelivery(int requestId)
        {
            await _automationService.SetRequestDeliveryStatus(requestId, ResourceStatus.InDelivery.ToString());

            return Ok();
        }

        [HttpPut("hr-actions/requests/{requestId}/delivered")]
        public async Task<IActionResult> SetRequestDelivered(int requestId)
        {
            await _automationService.SetRequestDeliveryStatus(requestId, ResourceStatus.Delivered.ToString());

            return Ok();
        }
    }
}
