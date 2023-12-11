using Skepta.Business.Util;

namespace Skepta.Business.Result
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
            int aantalWoorden = WordCounting(RandomText);
            WPM = Math.Ceiling(aantalWoorden / ElapsedTime.TotalSeconds * 60);
            
        }

        public void addMistake(char mistake)
        {
            char key = char.ToLower(mistake);
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
        private int WordCounting(string randomText)
        {
            if (string.IsNullOrWhiteSpace(randomText)) // Corrected variable name
                return 0;

            var amountOfWords = randomText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return amountOfWords.Length; // Return the count directly
        }

        
    }
}
