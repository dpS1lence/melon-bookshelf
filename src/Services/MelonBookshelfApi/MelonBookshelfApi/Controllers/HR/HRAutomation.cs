using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Controllers.HR
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class HRAutomation : Controller
    {
        [HttpPut("hrdashboard/requests/{requestId}/confirm")]
        public IActionResult ConfirmRequest([FromBody] int requestId)
        {
            return Ok();
        }

        [HttpPut("hrdashboard/requests/{requestId}/reject")]
        public IActionResult RejectRequest([FromBody] int requestId, string justification)
        {
            return Ok();
        }

        [HttpPut("hrdashboard/resources/{resourceId}/inprogress")]
        public IActionResult SetResourceInProgress([FromBody] int resourceId)
        {
            return Ok();
        }

        [HttpPut("hrdashboard/resources/{resourceId}/delivery")]
        public IActionResult SetResourceDelivery([FromBody] int resourceId)
        {
            return Ok();
        }

        [HttpPut("hrdashboard/resources/{resourceId}/delivered")]
        public IActionResult SetResourceDelivered([FromBody] int resourceId)
        {
            return Ok();
        }
    }
}
