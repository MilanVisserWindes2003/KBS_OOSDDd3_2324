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

        public ResultsLogic() { }
        public void aantalWoordenPerMinuut(string RandomText, TimeSpan ElapsedTime)
        {
            int aantalWoorden = WordCounting(RandomText);
            WPM = Math.Ceiling((aantalWoorden / ElapsedTime.TotalSeconds) * 60);
            
        }
        private int WordCounting(string randomText)
        {
            if (string.IsNullOrWhiteSpace(randomText)) // Corrected variable name
                return 0;

            var amountOfWords = randomText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return amountOfWords.Length; // Return the count directly
        }
    }
}
