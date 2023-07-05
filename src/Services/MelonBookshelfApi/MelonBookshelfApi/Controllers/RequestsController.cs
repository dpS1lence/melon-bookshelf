using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MelonBookshelfApi.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]
    public class RequestsController : Controller
    {
        [HttpPost]
        [Route("requests/wishlist")]
        public IActionResult AddWishlistResource()
        {
            return Ok();
        }
    }
}
