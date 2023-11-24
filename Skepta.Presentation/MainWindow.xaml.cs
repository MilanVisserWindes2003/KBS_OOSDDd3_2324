using Skepta.Business;
using Skepta.Business.Util;
using Skepta.Presentation.ViewModel;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Skepta.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel? viewModel;
        public MainWindow()
        {
            InitializeComponent();
            Logo.Source = new BitmapImage(new Uri("Skepta.Presentation\\Resources\\Img\\LogoBlack.png", UriKind.Relative));
            viewModel = new MainWindowViewModel();
            DataContext = viewModel;
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           viewModel.HandleKey(e.Key);
        }
    }
}
