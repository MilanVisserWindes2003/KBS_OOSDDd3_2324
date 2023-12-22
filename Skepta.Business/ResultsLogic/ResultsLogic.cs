using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Skepta.Business.Util;

namespace Skepta.Business.ResultsLogic
{
    public class ResultsLogic : ObservableObject
    {
        private double wpm;
        public double WPM
        {
            get { return wpm; }
            set { wpm = value; NotifyPropertyChanged(nameof(WPM)); }
        }

        public Dictionary<char, int> TypedMistake { get; set; }
        public char WorstMistake { get; private set; } = '-';

        public ResultsLogic() 
        {
            TypedMistake = new Dictionary<char, int>();
        }
        public void aantalWoordenPerMinuut(string RandomText, TimeSpan ElapsedTime)
        {
            double time = ElapsedTime.TotalSeconds;
            if (time == 0) 
            {
                time = 1;
            }
            int aantalWoorden = WordCounting(RandomText);
            WPM = Math.Ceiling((aantalWoorden / time) * 60);
        }
        public int WordCounting(string randomText)
        {
            if (string.IsNullOrWhiteSpace(randomText))
                return 0;
            var amountOfWords = randomText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return amountOfWords.Length; 
        }
        public void addMistake(char mistake)
        {

            char key = char.ToLower(mistake);
            if (key.Equals(' '))
            {
                key = '_';
            }
            if (TypedMistake.ContainsKey(key))
            {
                TypedMistake[key]++;
            }
            else
            {
                TypedMistake[key] = 1;
            }
            if (WorstMistake == '-')
            {
                WorstMistake = key;
                return;
            }
            if (TypedMistake.Count == 0 || TypedMistake[key] > TypedMistake[WorstMistake])
            {
                
                WorstMistake = key;
            }

        }

        public void EmptyDictionairy()
        {
            TypedMistake.Clear();
            WorstMistake = '-';
        }
        
    }
}
