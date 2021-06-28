using System;
using System.IO;
using HangmanMotorola;


namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = ("HangMan");
            try
            {
                string yesNo = string.Empty;
                do
                {
                    Console.WriteLine();
                    yesNo = PlayGame();
                } while (yesNo.ToUpper().Equals("Y"));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong! {e.Message}");
            }

            static string PlayGame()
            {
                var logger = new Logger();

                const int live = 5;
                ConsoleKeyInfo yesNo = new ConsoleKeyInfo();

                CountryAndCapitals countryAndCapitals = new CountryAndCapitals("./countries_and_capitals.txt");
                countryAndCapitals.FetchCountriesAndCapitals();
                countryAndCapitals.SelectRandomCountryAndCapital();

                Word word = new Word(countryAndCapitals.selectedCountryAndCapital.Value, live, false);

                Console.ForegroundColor = ConsoleColor.Yellow;
                logger.Log("Welcome to Hangman Game");
                logger.Log("Guess country name");
                logger.Log(word.Password());

                logger.Log("\n");
                logger.Log($"You have {live} live");
                Console.ForegroundColor = ConsoleColor.White;
                var game = new Game(DateTime.UtcNow);
                while (Result() == GameResult.Continue)
                {
                    if (word.livesToGuessWord == 1)
                    {
                        logger.Log($"The capital of {countryAndCapitals.selectedCountryAndCapital.Key}");
                    }
                    logger.Log("Would You like write a letter or whole word? \n L - if you select letter, another letter - if word");
                    var letterOrWord = Console.ReadLine();
                    var isLetterSelected = word.IsLetterSelected(letterOrWord);
                    var input = Console.ReadLine();

                    if (isLetterSelected)
                    {
                        word.ManageSelectedLetter(input);
                    }
                    else
                    {
                        word.ManageSelectedWord(input);
                    }
                }
                if (Result() == GameResult.Lose)
                {
                    logger.Log($"You guessed the capital after {word.letterGuessed.Count} letters. It took you {game.gameDuration().TotalSeconds} seconds");
                    logger.Log("Do you want to play again ? Y / N");
                    yesNo = Console.ReadKey();

                    countryAndCapitals.SelectRandomCountryAndCapital();
                    new Word(countryAndCapitals.selectedCountryAndCapital.Value, live, false);
                    return yesNo.KeyChar.ToString();

                }
                else
                {
                    logger.Log("You won!");
                    logger.Log("Please, write your name:");
                    var name = Console.ReadLine();
                    File.AppendAllText("ScoreGame.txt", $"{name} | {DateTime.UtcNow} | {game.gameDuration().TotalSeconds} \n");
                    logger.Log("Do you want to play again ? Y/N");
                    yesNo = Console.ReadKey();
                    return yesNo.KeyChar.ToString();
                }
                GameResult Result()
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
                        return GameResult.Continue;
                }
            }
        }
    }
}