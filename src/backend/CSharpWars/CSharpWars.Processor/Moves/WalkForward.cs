using System.Linq;
using CSharpWars.Enums;
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
        private const int STAMINA_ON_MOVE = 1;

        public WalkForward(BotProperties botProperties) : base(botProperties) { }

        public override BotResult Go()
        {
            // Build result based on current properties.
            var botResult = new BotResult(BotProperties);

            // Only perform move if enough stamina is available.
            if (BotProperties.CurrentStamina - STAMINA_ON_MOVE >= 0)
            {
                var destinationX = BotProperties.X;
                var destinationY = BotProperties.Y;

                switch (BotProperties.Orientation)
                {
                    case PossibleOrientations.North:
                        destinationY--;
                        break;
                    case PossibleOrientations.East:
                        destinationX++;
                        break;
                    case PossibleOrientations.South:
                        destinationY++;
                        break;
                    case PossibleOrientations.West:
                        destinationX--;
                        break;
                }

                if (!WillCollide(destinationX, destinationY))
                {
                    botResult.CurrentStamina -= STAMINA_ON_MOVE;
                    botResult.Move = PossibleMoves.WalkForward;
                    botResult.X = destinationX;
                    botResult.Y = destinationY;
                }
            }

            return botResult;
        }

        private bool WillCollide(int x, int y)
        {
            var collidingEdge = x < 0 || x >= BotProperties.Width || y < 0 || y >= BotProperties.Height;
            var collidingBot = BotProperties.Bots.FirstOrDefault(b => b.X == x && b.Y == y);
            return collidingBot != null || collidingEdge;
        }
    }
}