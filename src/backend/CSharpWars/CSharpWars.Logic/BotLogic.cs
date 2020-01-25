using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpWars.Common.Extensions;
using CSharpWars.Common.Helpers.Interfaces;
using CSharpWars.DataAccess;
using CSharpWars.Enums;
using CSharpWars.Logic.Interfaces;
using CSharpWars.Model;
using Microsoft.EntityFrameworkCore;

namespace CSharpWars.Logic
{
    public class BotLogic : IBotLogic
    {
        private readonly CSharpWarsDbContext _dbContext;
        private readonly IRandomHelper _randomHelper;
        private readonly IArenaLogic _arenaLogic;

        public BotLogic(
            CSharpWarsDbContext dbContext,
            IRandomHelper randomHelper,
            IArenaLogic arenaLogic)
        {
            _dbContext = dbContext;
            _randomHelper = randomHelper;
            _arenaLogic = arenaLogic;
        }

        public async Task<IList<Bot>> GetBots()
        {
            return await _dbContext.Bots.ToListAsync();
        }

        public async Task<IList<BotInfo>> GetBotInfo()
        {
            return await _dbContext.Bots.Select(b => new BotInfo
            {
                Id = b.Id,
                Name = b.Name,
                Orientation = b.Orientation,
                X = b.X,
                Y = b.Y,
                Move = b.Move,
                MaximumStamina = b.MaximumStamina,
                CurrentStamina = b.CurrentStamina
            }).ToListAsync();
        }

        public async Task<BotInfo> CreateBot(BotToCreate botToCreate)
        {
            var arena = await _arenaLogic.GetArena();
            var bots = await GetBots();

            var bot = new Bot
            {
                Name = botToCreate.Name,
                Orientation = _randomHelper.Get<PossibleOrientations>(),
                MaximumStamina = 100,
                CurrentStamina = 100,
                Move = PossibleMoves.Idling,
                Memory = new Dictionary<string, string>().Serialize(),
                Script = botToCreate.Script
            };

            var freeLocations = GetFreeLocation(arena, bots);
            (bot.X, bot.Y) = freeLocations[_randomHelper.Get(freeLocations.Count)];

            await _dbContext.Bots.AddAsync(bot);
            await _dbContext.SaveChangesAsync();

            return new BotInfo
            {
                Id = bot.Id,
                Name = bot.Name,
                Orientation = bot.Orientation,
                X = bot.X,
                Y = bot.Y,
                Move = bot.Move,
                MaximumStamina = bot.MaximumStamina,
                CurrentStamina = bot.CurrentStamina
            };
        }

        public async Task UpdateBots(IList<Bot> bots)
        {
            foreach (var bot in bots)
            {
                _dbContext.Entry(bot).Property(p => p.Script).IsModified = false;
            }

            await _dbContext.SaveChangesAsync();
        }

        private static IList<(int X, int Y)> GetFreeLocation(Arena arena, IList<Bot> bots)
        {
            var freeLocations = new List<(int X, int Y)>();

            for (var x = 0; x < arena.Width; x++)
            {
                for (var y = 0; y < arena.Height; y++)
                {
                    if (!bots.Any(b => b.X == x && b.Y == y))
                    {
                        freeLocations.Add((x, y));
                    }
                }
            }

            return freeLocations;
        }
    }
}