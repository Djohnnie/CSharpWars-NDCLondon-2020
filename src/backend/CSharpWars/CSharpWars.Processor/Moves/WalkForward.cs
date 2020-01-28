using System;
using CSharpWars.Processor.Middleware;
using CSharpWars.Scripting;

namespace CSharpWars.Processor.Moves
{
    /// <summary>
    /// Class containing logic for walking forward.
    /// </summary>
    /// <remarks>
    /// Performing this move makes the robot walk forward in the direction he is currently oriented.
    /// This move consumes one stamina point.
    /// </remarks>
    public class WalkForward : Move
    {
        public WalkForward(BotProperties botProperties) : base(botProperties) { }

        public override BotResult Go()
        {
            throw new NotImplementedException();
        }
    }
}