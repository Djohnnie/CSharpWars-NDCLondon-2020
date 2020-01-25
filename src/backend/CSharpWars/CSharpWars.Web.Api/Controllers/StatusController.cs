using System.Threading.Tasks;
using CSharpWars.Web.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CSharpWars.Web.Api.Controllers
{
    /// <summary>
    /// ASP.NET WebApi Controller containing endpoints for status checks.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ApiController
    {
        /// <summary>
        /// Endpoint that just returns an HTTP 200 OK.
        /// </summary>
        /// <returns>HTTP 200 OK.</returns>
        [HttpGet]
        public Task<IActionResult> GetStatus()
        {
            return Success();
        }
    }
}