using Business;
using Skepta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skepta.Presentation.ViewModel
{
    public class ResultaatViewModel : ViewModelBase
    {
        private SkeptaModel model;
        public ResultaatViewModel(SkeptaModel model) 
        { 
            this.model = model;
            // idk model.Text = model.GetTimerText();
        }
    }
}
