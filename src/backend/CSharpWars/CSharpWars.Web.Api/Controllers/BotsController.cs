using System.Threading.Tasks;
using CSharpWars.Logic.Interfaces;
using CSharpWars.Model;
using CSharpWars.Web.Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CSharpWars.Web.Api.Controllers
{
    /// <summary>
    /// ASP.NET WebApi Controller containing endpoints for bot functionality.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BotsController : ApiController<IBotLogic>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BotsController"/> class.
        /// </summary>
        /// <param name="botLogic">The bot logic.</param>
        /// <param name="memoryCache">The memory cache.</param>
        public BotsController(IBotLogic botLogic, IMemoryCache memoryCache) : base(botLogic, memoryCache) { }

        /// <summary>
        /// Endpoint that returns a list of all existing bots in the current game.
        /// </summary>
        /// <returns>HTTP 200 OK.</returns>
        [HttpGet]
        public Task<IActionResult> GetBots()
        {
            return Success(l => l.GetBotInfo());
        }

        /// <summary>
        /// Endpoint that creates and adds a new bot to the current game.
        /// </summary>
        /// <returns>HTTP 201 Created.</returns>
        [HttpPost]
        public Task<IActionResult> CreateBot([FromBody] BotToCreate botToCreate)
        {
            return Created(l => l.CreateBot(botToCreate));
        }
    }
}