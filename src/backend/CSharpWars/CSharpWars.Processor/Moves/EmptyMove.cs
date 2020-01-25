using CSharpWars.Processor.Middleware;
using CSharpWars.Scripting;

namespace CSharpWars.Processor.Moves
{
    public class EmptyMove : Move
    {
        public EmptyMove(BotProperties botProperties) : base(botProperties) { }

        public override BotResult Go()
        {
            var botResult = new BotResult(BotProperties)
            {
                Move = BotProperties.CurrentMove
            };
            
            return botResult;
        }
    }
}