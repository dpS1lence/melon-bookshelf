using MelonBookshelfApi.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using IResourceService = MelonBookshelfApi.Services.Contracts.IResourceService;

namespace MelonBookshelfApi.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class ResourcesController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly ILogger _logger;

        public ResourcesController(IResourceService resourceService, ILogger<ResourcesController> logger)
        {
            _resourceService = resourceService;
            _logger = logger;
        }

        [HttpGet]
        [Route("resources/search")]
        public async Task<IActionResult> SearchResources([FromBody] SearchResourcesRequestDto dto)
        {
            var resources = await _resourceService.SearchResources(dto.Type, dto.Category, dto.Title);

            var filteredResources = resources;

            if (!string.IsNullOrEmpty(dto.Type))
                filteredResources = filteredResources.Where(r => r.Type.ToString().Equals(dto.Type, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(dto.Category))
                filteredResources = filteredResources.Where(r => r.ResourceCategory.Name.Equals(dto.Category, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(dto.Title))
                filteredResources = filteredResources.Where(r => r.Title.Contains(dto.Title, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(filteredResources);
        }
    }
}
