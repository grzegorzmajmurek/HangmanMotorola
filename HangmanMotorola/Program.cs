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
                var game = new Game(DateTime.UtcNow);
                var ascii = new AsciiArt();

                
                ConsoleKeyInfo yesNo = new ConsoleKeyInfo();
                CountryAndCapitals countryAndCapitals = new CountryAndCapitals("./countries_and_capitals.txt");
                countryAndCapitals.FetchCountriesAndCapitals();
                countryAndCapitals.SelectRandomCountryAndCapital();

                const int live = 5;
                Word word = new Word(countryAndCapitals.selectedCountryAndCapital.Value, live, false);

                Console.ForegroundColor = ConsoleColor.Yellow;
                logger.Log("Welcome to Hangman Game");
                logger.Log("Guess country name");
                logger.Log(word.Password());

                logger.Log("\n");
                logger.Log($"You have {live} live");
                Console.ForegroundColor = ConsoleColor.White;
                
                while (game.Result(word) == GameResult.Continue)
                {
                    logger.Log($"{ascii.HangMan(word.livesToGuessWord + 1)}");
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

                if (game.Result(word) == GameResult.Lose)
                {
                    logger.Log($"You guessed {word.letterGuessed.Count} letters in capital word. It took you {game.GameDuration().TotalSeconds} seconds");
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
                    game.WriteResultToFile(name);

                    logger.Log($"The best scores: ");
                    game.FetchTheBestResultList().ForEach((result) => logger.Log(result));
                    logger.Log("Do you want to play again ? Y/N");
                    yesNo = Console.ReadKey();
                    return yesNo.KeyChar.ToString();
                }
            }
        }
    }
}