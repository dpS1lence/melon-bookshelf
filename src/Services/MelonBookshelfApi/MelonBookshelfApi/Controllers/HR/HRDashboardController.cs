using MelonBookshelfApi.Controllers.BaseUser;
using MelonBookshelfApi.ResponceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Controllers.HR
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class HRDashboardController : Controller
    {
        private readonly ILogger _logger;

        public HRDashboardController(ILogger<HRDashboardController> logger)
        {
            _logger = logger;
        }

        [HttpGet("hrdashboard/requests")]
        public IActionResult GetRequests()
        {
            return Ok();
        }

        [HttpGet("hrdashboard/resources")]
        public IActionResult GetAllResourcesForHR()
        {
            return Ok();
        }

        [HttpGet("hrdashboard/resources/{resourceId}")]
        public IActionResult GetResourceDetails([FromBody] int resourceId)
        {
            return Ok();
        }

        [HttpPost("hrdashboard/resources")]
        public IActionResult AddResource([FromBody] ResourceModel resource)
        {
            return Ok();
        }
    }
}
