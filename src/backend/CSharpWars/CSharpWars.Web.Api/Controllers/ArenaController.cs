using System.Threading.Tasks;
using CSharpWars.Logic.Interfaces;
using CSharpWars.Web.Api.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CSharpWars.Web.Api.Controllers
{
    /// <summary>
    /// ASP.NET WebApi Controller containing endpoints for arena functionality.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArenaController : ApiController<IArenaLogic>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArenaController"/> class.
        /// </summary>
        /// <param name="arenaLogic">The arena logic.</param>
        public ArenaController(IArenaLogic arenaLogic) : base(arenaLogic) { }

        /// <summary>
        /// Endpoint that returns the current configured arena dimensions.
        /// </summary>
        /// <returns>HTTP 200 OK.</returns>
        [HttpGet]
        public Task<IActionResult> GetArena()
        {
            return Success(l => l.GetArena());
        }
    }
}