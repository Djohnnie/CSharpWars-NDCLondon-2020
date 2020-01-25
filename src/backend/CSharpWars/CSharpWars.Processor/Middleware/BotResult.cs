using System;
using System.Collections.Generic;
using CSharpWars.Enums;
using CSharpWars.Scripting;

namespace CSharpWars.Processor.Middleware
{
    public class BotResult
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PossibleOrientations Orientation { get; set; }
        public int CurrentStamina { get; set; }
        public PossibleMoves Move { get; set; }
        public Dictionary<string, string> Memory { get; set; }


        public BotResult(BotProperties botProperties)
        {
            X = botProperties.X;
            Y = botProperties.Y;
            Orientation = botProperties.Orientation;
            CurrentStamina = botProperties.CurrentStamina;
            Move = PossibleMoves.Idling;
            Memory = botProperties.Memory;
        }
    }
}