using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Skepta.Presentation.View
{
    /// <summary>
    /// Interaction logic for ToetsenbordView.xaml
    /// </summary>
    public partial class ToetsenbordView : UserControl
    {

        public ToetsenbordView()
        {
            InitializeComponent();

            this.PreviewKeyDown += OnPreviewKeyDown;
            this.PreviewKeyUp += OnPreviewKeyUp;
            this.Loaded += ToetsenbordView_Loaded;
        }

        private void ToetsenbordView_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
            Keyboard.Focus(this);
        }
        public void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
           
            string letter = e.Key.ToString();
            Button knop = FindName(letter) as Button;

            if (knop != null)
            {
                knop.BorderThickness = new Thickness(6);
            }
        }

        public void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            string letter = e.Key.ToString();
            Button knop = FindName(letter) as Button;

            if (knop != null)
            {
                knop.BorderThickness = new Thickness(1);
            }
        }
    }
} 