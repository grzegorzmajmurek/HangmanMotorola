using System;
using System.Collections.Generic;

namespace HangmanMotorola
{
    class Word
    {
        public int livesToGuessWord { get; set; }
        public bool isCorrectWord { get; set; }
        public string selectedWord { get; set; }
        public List<string> letterGuessed = new List<string>();
        public List<string> letterNotInWord = new List<string>();
        public Word(string word, int lives, bool isCorrect)
        {
            selectedWord = word;
            livesToGuessWord = lives;
            isCorrectWord = isCorrect;
        }

        public string Password(List<string> letters = null)
        {
            var password = "";
            foreach (char c in this.selectedWord)
            {
                char space = ' ';
                var l = c.Equals(space) ? " " : "-";
                var newLetter = letters != null && letters.Contains(c.ToString().ToLower()) ? c.ToString() : l;
                password = password + " " + newLetter;
            }
            return password;
        }

        private bool IsLetterInWord(string letter)
        {
            var Logger = new Logger();
            var isOneLetter = letter.Length == 1;
            var isImplicitIn = selectedWord.Contains(letter, StringComparison.InvariantCultureIgnoreCase);
            if (isImplicitIn && isOneLetter)
                Logger.Log("Congratulation, This letter is correct!");
            else
                Logger.Log("So bad, try again!");
            return isImplicitIn;
        }

        public bool IsLetterSelected(string input)
        {
            var logger = new Logger();
            bool isLetterSelected = input.ToLower() == 'l'.ToString();
            if (isLetterSelected)
                logger.Log("Write a letter:");
            else
                logger.Log("Write a whole word:");

            return isLetterSelected;
        }

        public void ManageSelectedLetter(string input)
        {
            var logger = new Logger();
            if (IsLetterInWord(input))
            {
                letterGuessed.Add(input.ToLower());
                logger.Log(Password(letterGuessed));
                if (!Password(letterGuessed).Contains("-"))
                {
                    isCorrectWord = true;
                }
            }
            else
            {
                livesToGuessWord -= 1;
                if (input.Length != 1) { return; }
                letterNotInWord.Add(input);
                logger.Log("Not in word:");
                letterNotInWord.ForEach(letter => logger.Log($"* {letter}"));
                logger.Log("\n");
            }
        }

        public void ManageSelectedWord(string input)
        {
            var logger = new Logger();
            if (input.ToLower() == selectedWord.ToLower())
            {
                isCorrectWord = true;
                logger.Log("You win! Word is correct!");
            }
            else
            {
                livesToGuessWord -= 2;
                logger.Log("Word is not correct!");
            }
        }
    }
}
