using MelonBookshelfApi.RequestModels;
using MelonBookshelfApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MelonBookshelfApi.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly ILogger _logger;

        public RequestsController(IRequestService requestService, ILogger<RequestsController> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }

        [HttpPost]
        [Route("requests")]
        public async Task<IActionResult> AddRequest([FromBody] ResourceRequestDto requestDto)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var result = await _requestService.ProcessRequestAsync(requestDto, userId);

            return Ok(result);
        }

    }
}
