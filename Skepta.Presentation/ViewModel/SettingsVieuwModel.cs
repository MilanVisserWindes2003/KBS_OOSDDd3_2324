using Skepta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skepta.Presentation.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        public SettingsViewModel(SkeptaModel model)
        {
            this.model = model;
        }
    }
}
