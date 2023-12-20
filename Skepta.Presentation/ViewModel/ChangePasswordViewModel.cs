using Skepta.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataAccess;
using Skepta.Presentation.ViewModel.Commands;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    internal class ChangePasswordViewModel : ViewModelBase
    {
        private SkeptaModel model;

        public ChangePasswordViewModel(SkeptaModel model)
        {
            this.model = model;
        }
        public ICommand BackFromChange => new BaseCommand(BackChangeCmd);
        public ICommand ChangePasswordCommand => new BaseCommand(ChangePasswordCmd);

        private void ChangePasswordCmd()
        {
            string loggedInUsername = model.Username; // Gebruik de huidige ingelogde gebruikersnaam
            DataAccess.DataAccess dataAccess = new DataAccess.DataAccess();
        }

        private void BackChangeCmd()
        {
            RequestPage = PageId.Settings;
        }
    }
}
