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

namespace Skepta.Presentation.ViewModel
{
    public class ResultaatViewModel : ViewModelBase
    {
        private SkeptaModel model;
        private double wpm;

        public ICommand LoadedCommand { get; }
        public double WPM
        {
            get { return wpm; }
            set { wpm = value; NotifyPropertyChanged(nameof(WPM)); }
        }

        public ResultaatViewModel(SkeptaModel model) 
        { 
            this.model = model;         
            this.model.aantalWoordenPerMinuut();
            // idk model.Text = model.GetTimerText();
        }
       
       

        //public void aantalWoordenPerMinuut() => WPM = Math.Ceiling((model.aantalWoordenPerMinuut() / 10) * 60);

    }
}
