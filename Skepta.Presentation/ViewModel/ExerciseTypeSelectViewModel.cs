using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    public class ExerciseTypeSelectViewModel : ViewModelBase
    {
        private readonly SkeptaModel model;
        public ExerciseTypeSelectViewModel(SkeptaModel model)
        {
            this.model = model;
        }

        public ICommand Speech => new BaseCommand(() => RequestPage = PageId.TextToSpeech);
        public ICommand Text => new BaseCommand(() => RequestPage = PageId.TextShuffle);


    }
}
