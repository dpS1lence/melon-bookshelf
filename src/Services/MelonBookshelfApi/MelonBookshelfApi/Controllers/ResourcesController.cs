using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class ResourcesController : Controller
    {
        [HttpPut]
        [Route("resources/{resourceId}/return")]
        public IActionResult ReturnResource(int resourceId)
        {
            return Ok();
        }
    }
}
