using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApiRestful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {

            var msg = HttpContext.Features.Get<IExceptionHandlerFeature>();

            int statusCode = HttpContext.Response.StatusCode;

            return Ok();
        }
    }
}
