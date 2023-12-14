using Org.BouncyCastle.Asn1.Mozilla;
using Org.BouncyCastle.Security;
using Skepta.Business;
using Skepta.DataAcces.HistoryFolder;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    public class GeschiedenisViewModel : ViewModelBase
    {
        private DataTable historyTable;
        private SkeptaModel model;

        public History history { get; private set; }
        private string historyDisplay;
        public string HistoryDisplay
        {
            get => historyDisplay;
            set
            {
                historyDisplay = value;
                
                NotifyPropertyChanged(nameof(HistoryDisplay));
            }
        }
        public DataTable HistoryTable
        {
            get => historyTable;
            set
            {
                historyTable = value;

                NotifyPropertyChanged(nameof(HistoryTable));
            }

        }
        public GeschiedenisViewModel(SkeptaModel model)
        {
            this.model = model;
        }
        public ICommand Back => new BaseCommand(() => RequestPage = PageId.MenuScreen);


        public override void OpenPage()
        {

            history = model.ObtainHistory(model.Username);
            HistoryTable = history.HistoryTable;
            
        }

    }
}
