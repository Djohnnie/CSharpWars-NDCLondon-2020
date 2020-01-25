namespace CSharpWars.Model
{
    /// <summary>
    /// Model class representing a bot that needs to be created.
    /// </summary>
    public class BotToCreate
    {
        /// <summary>
        /// The name for a bot.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A Base64 encoded C# script containing the
        /// logic that needs to be executed by this bot.
        /// </summary>
        public string Script { get; set; }
    }
}