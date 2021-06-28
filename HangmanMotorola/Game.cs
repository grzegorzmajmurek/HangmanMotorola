using System;

namespace HangmanMotorola
{
    class Game
    {
        private DateTime startGame;
        public Game(DateTime startGame)
        {
            this.startGame = startGame;
        }

        public TimeSpan gameDuration()
        {
            return DateTime.UtcNow - startGame;
        }
    }
}
