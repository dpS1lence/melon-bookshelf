using MelonBookshelfApi.RequestDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MelonBookshelfApi.Controllers.BaseUser
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class BaseUserAutomationController : Controller
    {
        private readonly ILogger _logger;

        public BaseUserAutomationController(ILogger<ResourcesDataController> logger)
        {
            _logger = logger;
        }

        [HttpPut]
        [Route("user-automation/{resourceId}/return")]
        public async Task<IActionResult> ReturnPhysicalResource([FromBody] int resourceId)
        {
            return Ok();
        }

        //[HttpPut]
        //[Route("user-automation/{resourceId}/return")]
        //public async Task<IActionResult> ReturnPhysicalResource([FromBody] int resourceId)
        //{
        //    return Ok();
        //}
    }
}
