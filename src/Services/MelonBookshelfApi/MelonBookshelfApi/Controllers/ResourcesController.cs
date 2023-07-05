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

        public ResourcesController(IResourceService resourceService, ILogger logger)
        {
            _resourceService = resourceService;
            _logger = logger;
        }

        [HttpGet]
        [Route("resources/search")]
        public async Task<IActionResult> SearchResources([FromBody] string? type, string? category, string? title)
        {
            var resources = await _resourceService.SearchResources(type, category, title);

            var filteredResources = resources;

            if (!string.IsNullOrEmpty(type))
                filteredResources = filteredResources.Where(r => r.Type.ToString().Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(category))
                filteredResources = filteredResources.Where(r => r.ResourceCategory.Name.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(title))
                filteredResources = filteredResources.Where(r => r.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(filteredResources);
        }
    }
}
