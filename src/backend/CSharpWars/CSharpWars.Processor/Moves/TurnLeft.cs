using CSharpWars.Enums;
using CSharpWars.Processor.Middleware;
using CSharpWars.Scripting;

namespace CSharpWars.Processor.Moves
{
    /// <summary>
    /// Class containing logic for turning left.
    /// </summary>
    /// <remarks>
    /// Performing this move makes the robot turn anti-clockwise by 90°.
    /// This move does not consume stamina because the robot will not move away from its current location in the arena grid.
    /// </remarks>
    public class TurnLeft : Move
    {
        public TurnLeft(BotProperties botProperties) : base(botProperties) { }

        public override BotResult Go()
        {
            // Build result based on current properties.
            var botResult = new BotResult(BotProperties)
            {
                Move = PossibleMoves.TurningLeft
            };

            botResult.Orientation = BotProperties.Orientation switch
            {
                PossibleOrientations.North => PossibleOrientations.West,
                PossibleOrientations.East => PossibleOrientations.North,
                PossibleOrientations.South => PossibleOrientations.East,
                PossibleOrientations.West => PossibleOrientations.South,
                _ => botResult.Orientation
            };

            return botResult;
        }
    }
}