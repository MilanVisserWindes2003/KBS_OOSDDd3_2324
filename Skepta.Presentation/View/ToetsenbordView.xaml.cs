using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Skepta.Presentation.View
{

    public partial class ToetsenbordView : UserControl
    {

        public ToetsenbordView()
        {
            InitializeComponent();

            this.PreviewKeyDown += OnPreviewKeyDown;
            this.PreviewKeyUp += OnPreviewKeyUp;
            this.Loaded += ToetsenbordView_Loaded;
        }

        //Makes it so that the keyboard becomes the focus of the screen
        private void ToetsenbordView_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();
            Keyboard.Focus(this);
        }
        //If a key is presses this key becomes highlighted
        public void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
           
            string letter = e.Key.ToString();
            Button knop = FindName(letter) as Button;

            if (knop != null)
            {
                knop.BorderThickness = new Thickness(6);
            }
        }

        //If a key is let go it gets to the key becomes default again.
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