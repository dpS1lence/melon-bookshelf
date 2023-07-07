using MelonBookshelfApi.RequestDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Security.Claims;
using IResourceService = MelonBookshelfApi.Services.Contracts.IResourceService;

namespace MelonBookshelfApi.Controllers.BaseUser
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class ResourcesDataController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly ILogger _logger;

        public ResourcesDataController(IResourceService resourceService, ILogger<ResourcesDataController> logger)
        {
            _resourceService = resourceService;
            _logger = logger;
        }

        [HttpGet]
        [Route("resources")]
        public IActionResult Resources()
        {
            var resources = _resourceService.GetAllResources();

            return Ok(resources);
        }

        [HttpGet]
        [Route("resources/search")]
        public async Task<IActionResult> SearchResources([FromBody] SearchResourcesDto dto)
        {
            var resources = await _resourceService.SearchResources(dto.Type, dto.Category, dto.Title);

            var filteredResources = resources;

            if (!string.IsNullOrEmpty(dto.Type))
                filteredResources = filteredResources.Where(r => r.Type.ToString().Equals(dto.Type, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(dto.Category))
                filteredResources = filteredResources.Where(r => r.ResourceCategory.Equals(dto.Category, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(dto.Title))
                filteredResources = filteredResources.Where(r => r.Title.Contains(dto.Title, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(filteredResources);
        }

        [HttpGet]
        [Route("resources/categories")]
        public IActionResult GetResourcesCategories()
        {
            var categories = _resourceService.GetCategoriesAsync();

            return Ok(categories);
        }

        [HttpGet]
        [Route("resources/{physical-resource-id}")]
        public async Task<IActionResult> PhysicalResourceById(string pysicalResourceId)
        {
            var physicalResource = await _resourceService.GetPhysicalResourceByIdAsync(pysicalResourceId);

            return Ok(physicalResource);
        }

        [HttpGet]
        [Route("resources/{resource-id}")]
        public async Task<IActionResult> ResourceById(string resourceId)
        {
            var physicalResource = await _resourceService.GetResourceByIdAsync(resourceId);

            return Ok(physicalResource);
        }

        [HttpGet]
        [Route("resources/{userId}/taken")]
        public async Task<IActionResult> PhysicalResourcesByUserId(string userId)
        {
            var physicalResources = await _resourceService.GetPhysicalResourcesByUserIdAsync(userId);

            return Ok(physicalResources);
        }
    }
}
