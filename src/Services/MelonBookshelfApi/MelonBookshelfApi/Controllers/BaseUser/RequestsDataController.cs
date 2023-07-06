using MelonBookshelfApi.CustomObjects.Enums;
using MelonBookshelfApi.RequestDtos;
using MelonBookshelfApi.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MelonBookshelfApi.Controllers.BaseUser
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
        [Route("requests/create")]
        public async Task<IActionResult> AddRequest([FromBody] ResourceRequestDto requestDto)
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var result = await _requestService.ProcessRequestAsync(requestDto, userId);
            if (result.ProcessRequest == ProcessRequest.UnableToProcessRequest)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (result.ProcessRequest == ProcessRequest.ContentAlreadyExists)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            else if (result.ProcessRequest == ProcessRequest.ContentRequestInProgress)
            {
                return StatusCode(StatusCodes.Status208AlreadyReported, result.RequestId);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("requests/get-by-userid")]
        public async Task<IActionResult> GetRequestsByUserId()
        {
            var userId = User.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;

            var result = await _requestService.GetRequestsByUserId(userId);

            return Ok(result);
        }

        [HttpGet]
        [Route("requests/{requestId}")]
        public async Task<IActionResult> GetRequestById([FromBody] int requestId)
        {
            var result = await _requestService.GetRequestById(requestId);

            return Ok(result);
        }
    }
}
