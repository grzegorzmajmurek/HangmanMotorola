using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HangmanMotorola
{
    class CountryAndCapitals
    {
        public Dictionary<string, string> countryAndCapitalsList = new Dictionary<string, string>();
        public KeyValuePair<string, string> selectedCountryAndCapital = new KeyValuePair<string, string>();
        private string filePath { get; set; }
        public CountryAndCapitals(string path)
        {
            filePath = path;
        }
        public void FetchCountriesAndCapitals()
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var country = s.Split("| ").First();
                    var capital = s.Split("| ").Last();

                    countryAndCapitalsList.Add(country, capital);
                }
            }
        }
        public void SelectRandomCountryAndCapital()
        {
            var random = new Random();
            selectedCountryAndCapital = countryAndCapitalsList.ElementAt(random.Next(0, countryAndCapitalsList.Count)); ;
        }
    }
}
