using System.Collections.Generic;
using System.Linq;

namespace HangmanMotorola
{
    class AsciiArt
    {
        private List<string> ascii = new List<string>();

       public AsciiArt()
        {
            ascii.Add("  +---+\n  |   |\n  O   |\n /|   |\n /    |\n      |\n=========");
            ascii.Add("  +---+\n  |   |\n  O   |\n /|   |\n /    |\n      |\n=========");
            ascii.Add("  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========");
            ascii.Add("  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========");
            ascii.Add("  +---+\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========");
            ascii.Add("  +---+\n  |   |\n  O   |\n      |\n      |\n      |\n=========");
            ascii.Add("  +---+\n  |   |\n      |\n      |\n      |\n      |\n=========");
        }

        public string HangMan(int index)
        {
            return ascii.ElementAt(index);
        }
    }
}
