using System.Threading.Tasks;
using CSharpWars.Common.Extensions;
using CSharpWars.Processor.Middleware.Interfaces;
using CSharpWars.Processor.Moves;

namespace CSharpWars.Processor.Middleware
{
    public class Postprocessor : IPostprocessor
    {
        public Task Go(ProcessingContext context)
        {
            foreach (var bot in context.Bots)
            {
                var botProperties = context.GetBotProperties(bot.Id);

                var botResult = Move.Build(botProperties).Go();
                bot.Orientation = botResult.Orientation;
                bot.X = botResult.X;
                bot.Y = botResult.Y;
                bot.CurrentStamina = botResult.CurrentStamina < 0 ? 0 : botResult.CurrentStamina;
                bot.Move = botResult.Move;
                bot.Memory = botResult.Memory.Serialize();
            }

            return Task.CompletedTask;
        }
    }
}