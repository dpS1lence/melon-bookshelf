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
            var result = await _requestService.ProcessRequestAsync(requestDto, requestDto.UserId);

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
        [Route("requests/{userId}/get-by-userid")]
        public async Task<IActionResult> GetRequestsByUserId(string userId)
        {
            var result = await _requestService.GetRequestsByUserId(userId);

            return Ok(result);
        }

		[HttpGet]
		[Route("requests")]
		public async Task<IActionResult> GetRequests()
		{
			var result = await _requestService.GetRequests();

			return Ok(result);
		}

		[HttpGet]
        [Route("requests/{requestId}")]
        public async Task<IActionResult> GetRequestById(int requestId)
        {
            var result = await _requestService.GetRequestById(requestId);

            return Ok(result);
        }
    }
}
