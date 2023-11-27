using Business;
using Skepta.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skepta.Presentation.ViewModel
{
    public class ResultaatViewModel : ViewModelBase
    {
        private SkeptaModel model;
        private double wpm;
        public double WPM
        {
            get { return wpm; }
            set { wpm = value; NotifyPropertyChanged(nameof(WPM)); }
        }

        public ResultaatViewModel(SkeptaModel model) 
        { 
            this.model = model;
            aantalWoordenPerMinuut();
            // idk model.Text = model.GetTimerText();
        }
        public void aantalWoordenPerMinuut() => WPM = Math.Ceiling((model.aantalWoorden / 10) * 60);

    }
}
