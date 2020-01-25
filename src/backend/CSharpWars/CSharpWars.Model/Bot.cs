using System;
using CSharpWars.Enums;

namespace CSharpWars.Model
{
    /// <summary>
    /// Model class representing a bot in an active game.
    /// </summary>
    public class Bot
    {
        /// <summary>
        /// A unique identifier for this bot.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// A name representing this bot.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The location on the X-ax in the arena this bot is occupying.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The location on the Y-ax in the arena this bot is occupying.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The orientation this bot is facing.
        /// </summary>
        public PossibleOrientations Orientation { get; set; }

        /// <summary>
        /// The maximum stamina this bot has available.
        /// </summary>
        public int MaximumStamina { get; set; }

        /// <summary>
        /// The current stamina this bot has left.
        /// </summary>
        public int CurrentStamina { get; set; }

        /// <summary>
        /// The current move this bot is performing.
        /// </summary>
        public PossibleMoves Move { get; set; }

        /// <summary>
        /// The JSON serialized memory this bot is storing.
        /// </summary>
        public string Memory { get; set; }

        /// <summary>
        /// A Base64 encoded C# script containing the
        /// logic that needs to be executed by this bot.
        /// </summary>
        public string Script { get; set; }
    }
}