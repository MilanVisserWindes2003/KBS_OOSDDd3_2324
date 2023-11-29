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
using Skepta.Business.ResultsLogic;
using Skepta.Presentation.ViewModel.Commands;

namespace Skepta.Presentation.ViewModel
{
    public class ResultaatViewModel : ViewModelBase
    {
        private SkeptaModel model;
        private ResultsLogic rsl;
        private double wpm;

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
        
        public ResultaatViewModel(SkeptaModel model) 
        { 
            this.model = model; 
             rsl = new ResultsLogic();
            //this.model.aantalWoordenPerMinuut();
            // idk model.Text = model.GetTimerText();
        }

        public override void OpenPage()
        {
            rsl.aantalWoordenPerMinuut(model.RandomText, model.ElapsedTime);
            wpmValue = rsl.WPM;
        }

        public ICommand back => new BaseCommand(() => RequestPage = PageId.Exercise);

    }
}
