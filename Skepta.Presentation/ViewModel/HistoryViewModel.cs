using Skepta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skepta.Presentation.ViewModel
{
    public class HistoryViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        public HistoryViewModel(SkeptaModel model)
        {
            // moet weg
            this.model = model;

        }
    }
}
