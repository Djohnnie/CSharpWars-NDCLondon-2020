namespace CSharpWars.Enums
{
    /// <summary>
    /// Enumeration type containing all possible bot moves.
    /// </summary>
    public enum PossibleMoves
    {
        /// <summary>
        /// Performing no move, thus standing idle.
        /// </summary>
        Idling,

        /// <summary>
        /// Turning 90° to the left.
        /// </summary>
        TurningLeft,

        /// <summary>
        /// Turning 90° to the right.
        /// </summary>
        TurningRight,

        /// <summary>
        /// Turning 180° around.
        /// </summary>
        TurningAround,

        /// <summary>
        /// Walking forward one tile.
        /// </summary>
        WalkForward,

        /// <summary>
        /// The bot-script encountered a runtime error. 
        /// </summary>
        ScriptError
    }
}