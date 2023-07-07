using MelonBookshelfApi.Controllers.BaseUser;
using MelonBookshelfApi.ResponceModels;
using MelonBookshelfApi.Services.Contracts;
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
        private readonly IRequestService _requestService;
        private readonly IResourceService _resourceService;

        public HRDashboardController(ILogger<HRDashboardController> logger, IRequestService requestService, IResourceService resourceService)
        {
            _logger = logger;
            _requestService = requestService;
            _resourceService = resourceService;
        }

        [HttpGet("hrdashboard/requests")]
        public IActionResult GetRequests()
        {
            var requests = _requestService.GetRequests();

            return Ok(requests);
        }

        [HttpGet("hrdashboard/resources")]
        public IActionResult GetAllResourcesForHR()
        {
            var resources = _resourceService.GetAllResourcesHR();

            return Ok(resources);
        }

        [HttpPost("hrdashboard/create-resource")]
        public async Task<IActionResult> AddResource([FromBody] ResourceHRModel model)
        {
            await _resourceService.AddResource(model);

            return Ok();
        }
    }
}
