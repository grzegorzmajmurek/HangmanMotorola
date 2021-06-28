using System;
using System.Collections.Generic;
using System.IO;

namespace HangmanMotorola
{
    class Game
    {
        private DateTime startGame;
        private static string fileName = "ScoreGame.txt";
        public Game(DateTime startGame)
        {
            this.startGame = startGame;
        }

        public TimeSpan GameDuration()
        {
            return DateTime.UtcNow - startGame;
        }

        public void WriteResultToFile(string name)
        {
            File.AppendAllText(fileName, $"{name} | {DateTime.UtcNow} | {GameDuration().TotalSeconds} \n");
        }

        public GameResult Result(Word word)
        {
            if (word.livesToGuessWord <= 0)
            {
                return GameResult.Lose;
            }
            else if (word.isCorrectWord)
            {
                return GameResult.Win;
            }
            else
            {
                return GameResult.Continue;
            }
        }

        public List<string> FetchTheBestResultList()
        {
            var theBestResult = new List<string>();
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    theBestResult.Add(s);
                }
            }

            return theBestResult;
        }

    }
}
