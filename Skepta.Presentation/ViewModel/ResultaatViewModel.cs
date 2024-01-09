using Skepta.Business;
using Skepta.Business.ResultsLogic;
using Skepta.DataAcces.HistoryFolder;
using Skepta.Presentation.ViewModel.Commands;
using System.Windows.Input;


namespace Skepta.Presentation.ViewModel
{
    public class ResultaatViewModel : ViewModelBase
    {
        private SkeptaModel model;
        private ResultsLogic rsl;
        private double wpm;
        private char mistake;

        public string Typespeed { get; set; }

        public double wpmValue

        {
            get
            {
                return wpm;
            }
            set
            {
                wpm = value;
                NotifyPropertyChanged(nameof(wpmValue));
            }
        }

        public char Mistake
        {
            get { return mistake; }
            set 
            { 
                mistake = value;
                NotifyPropertyChanged(nameof(Mistake));
            }
        }
        
        public ResultaatViewModel(SkeptaModel model) 
        { 
            this.model = model;
            rsl = model.result;
        }

        // the amount of words per minute that the user scored is recorded and local property's are set to reflect words per minute and the mistakes made this data is also logged in the database
        public override void OpenPage()
        {
            rsl.aantalWoordenPerMinuut(model.RandomText, model.ElapsedTime);
            wpmValue = rsl.WPM;
            Typespeed = rsl.WPM.ToString();         
            Mistake = rsl.WorstMistake;
            InsertHistory();
        }

        public ICommand back => new BaseCommand(BackCmd);

        //method returns to the menu screen and clears current results of the user
        private void BackCmd()
        {
            rsl.EmptyDictionairy();
            RequestPage = PageId.MenuScreen;
        }

        // results of the user are inserted into the database
        private void InsertHistory()
        {
            History history = new History();
            history.IsSpoken = model.IsSpeechExercise;
            history.TextId = model.ObtainTextId(model.TextDifficulty, model.TextLength.ToString(), model.RandomText);
            history.WorstMistake = Mistake;
            history.TypeSpeed = Typespeed;
            history.Username = model.Username;
            model.InsertHistoryData(history);
        }

    }
}
