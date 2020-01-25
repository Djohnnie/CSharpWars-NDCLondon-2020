using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpWars.Model;
using CSharpWars.Processor.Middleware.Interfaces;
using CSharpWars.Scripting;

namespace CSharpWars.Processor.Middleware
{
    public class Preprocessor : IPreprocessor
    {
        public Task<ProcessingContext> Go(Arena arena, IList<Bot> bots)
        {
            var processingContext = new ProcessingContext(arena, bots);

            foreach (var bot in bots)
            {
                var botProperties = new BotProperties(arena, bot, bots);
                processingContext.AddBotProperties(bot.Id, botProperties);
            }

            return Task.FromResult(processingContext);
        }
    }
}