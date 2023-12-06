using Business;
using GalaSoft.MvvmLight.Command;
using Skepta.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Skepta.Presentation.ViewModel.Commands;
using Skepta.Business.Result;

namespace Skepta.Presentation.ViewModel
{
    public class ResultaatViewModel : ViewModelBase
    {
        private SkeptaModel model;
        private ResultsLogic rsl;
        private double wpm;
        private char mistake;

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
            //this.model.aantalWoordenPerMinuut();
            // idk model.Text = model.GetTimerText();
        }

        public override void OpenPage()
        {
            rsl.aantalWoordenPerMinuut(model.RandomText, model.ElapsedTime);
            wpmValue = rsl.WPM;
            Mistake = rsl.WorstMistake;
        }

        public ICommand back => new BaseCommand(BackCmd);

        private void BackCmd()
        {
            rsl.EmptyDictionairy();
            RequestPage = PageId.Exercise;
        }

    }
}
