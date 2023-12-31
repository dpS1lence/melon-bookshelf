﻿using MelonBookshelfApi.Controllers.BaseUser;
using MelonBookshelfApi.ResponceModels;
using MelonBookshelfApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Controllers.HR
{
    [ApiController]
    [Route("api")]
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> GetRequests()
        {
            var requests = await _requestService.GetRequests();

            return Ok(requests);
        }

        [HttpGet("hrdashboard/resources")]
        public async Task<IActionResult> GetAllResourcesForHR()
        {
            var resources = await _resourceService.GetAllResourcesHR();

            return Ok(resources);
        }
    }
}
