using CSharpWars.Common.Extensions;
using CSharpWars.Enums;

namespace CSharpWars.Scripting
{
    public class ScriptGlobals
    {
        private readonly BotProperties _botProperties;

        public int Width => _botProperties.Width;
        public int Height => _botProperties.Height;
        public int X => _botProperties.X;
        public int Y => _botProperties.Y;
        public PossibleOrientations Orientation => _botProperties.Orientation;
        public PossibleMoves LastMove => _botProperties.LastMove;
        public int MaximumStamina => _botProperties.MaximumStamina;
        public int CurrentStamina => _botProperties.CurrentStamina;

        public ScriptGlobals(BotProperties botProperties)
        {
            _botProperties = botProperties;
        }

        /// <summary>
        /// Calling this method will move the player one position forward.
        /// </summary>
        public void WalkForward()
        {
            SetCurrentMove(PossibleMoves.WalkForward);
        }

        /// <summary>
        /// Calling this method will turn the player 90 degrees to the left.
        /// </summary>
        public void TurnLeft()
        {
            SetCurrentMove(PossibleMoves.TurningLeft);
        }

        /// <summary>
        /// Calling this method will turn the player 90 degrees to the right.
        /// </summary>
        public void TurnRight()
        {
            SetCurrentMove(PossibleMoves.TurningRight);
        }

        /// <summary>
        /// Calling this method will turn the player 180 degrees around.
        /// </summary>
        public void TurnAround()
        {
            SetCurrentMove(PossibleMoves.TurningAround);
        }

        /// <summary>
        /// Calling this method will store information into the players memory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void StoreInMemory<T>(string key, T value)
        {
            if (_botProperties.Memory.ContainsKey(key))
            {
                _botProperties.Memory[key] = value.Serialize();
            }
            else
            {
                _botProperties.Memory.Add(key, value.Serialize());
            }
        }

        /// <summary>
        /// Calling this method will load information from the players memory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T LoadFromMemory<T>(string key)
        {
            if (_botProperties.Memory.ContainsKey(key))
            {
                return _botProperties.Memory[key].Deserialize<T>();
            }

            return default;
        }

        private void SetCurrentMove(PossibleMoves currentMove)
        {
            if (_botProperties.CurrentMove == PossibleMoves.Idling)
            {
                _botProperties.ChangeCurrentMove(currentMove);
            }
        }
    }
}