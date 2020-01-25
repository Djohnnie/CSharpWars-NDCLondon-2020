using System;
using System.Collections.Generic;
using CSharpWars.Common.Extensions;
using CSharpWars.Enums;
using CSharpWars.Model;

namespace CSharpWars.Scripting
{
    public class BotProperties
    {
        public Guid BotId { get; }
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public int X { get; }
        public int Y { get; }
        public PossibleOrientations Orientation { get; }
        public PossibleMoves LastMove { get; }
        public PossibleMoves CurrentMove { get; private set; }
        public int MaximumStamina { get; }
        public int CurrentStamina { get; }
        public IList<Bot> Bots { get; }
        public Dictionary<string, string> Memory { get; }

        public BotProperties(Arena arena, Bot bot, IList<Bot> bots)
        {
            BotId = bot.Id;
            Name = bot.Name;
            Width = arena.Width;
            Height = arena.Height;
            X = bot.X;
            Y = bot.Y;
            Orientation = bot.Orientation;
            LastMove = bot.Move;
            CurrentMove = PossibleMoves.Idling;
            MaximumStamina = bot.MaximumStamina;
            CurrentStamina = bot.CurrentStamina;
            Bots = bots;
            Memory = bot.Memory.Deserialize<Dictionary<string, string>>();
        }

        public void ChangeCurrentMove(PossibleMoves move)
        {
            CurrentMove = move;
        }
    }
}