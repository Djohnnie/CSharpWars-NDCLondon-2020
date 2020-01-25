using System;
using System.Collections.Generic;
using CSharpWars.Enums;
using CSharpWars.Processor.Middleware;
using CSharpWars.Scripting;

namespace CSharpWars.Processor.Moves
{

    public abstract class Move
    {
        private static readonly Dictionary<PossibleMoves, Func<BotProperties, Move>> _moves = new Dictionary<PossibleMoves, Func<BotProperties, Move>>
        {
            { PossibleMoves.WalkForward, p => new WalkForward(p) },
            { PossibleMoves.TurningLeft, p => new TurnLeft(p) },
            { PossibleMoves.TurningRight, p => new TurnRight(p) },
            { PossibleMoves.TurningAround, p => new TurnAround(p) },
            { PossibleMoves.Idling, p => new EmptyMove(p) },
            { PossibleMoves.ScriptError, p => new EmptyMove(p) },
        };

        protected readonly BotProperties BotProperties;

        protected Move(BotProperties botProperties)
        {
            BotProperties = botProperties;
        }

        public static Move Build(BotProperties botProperties)
        {
            return _moves[botProperties.CurrentMove](botProperties);
        }

        public abstract BotResult Go();
    }
}