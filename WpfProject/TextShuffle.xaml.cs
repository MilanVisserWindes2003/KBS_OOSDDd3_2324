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

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for TextShuffle.xaml
    /// </summary>
    public partial class TextShuffle : Page
    {
        //reshuffle pagina heb je een varriable
        Business.Business business;

        public TextShuffle(Business.Business business)
        {
            InitializeComponent();
            this.business = business;
            this.DataContext = business;
            TextBlock.Text = business.obtainRandomText(business.TextDifficulty, business.TextLength);
        }

        private void Shuffle_Click(object sender, RoutedEventArgs e)
        {
            TextBlock.Text = business.obtainRandomText(business.TextDifficulty, business.TextLength);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (business.IsSpeechExercise)
            {
                NavigationService.Navigate(new SpeechExcersize(business));
            }
            NavigationService.Navigate(new TextExcersize(business));
        }
    }
}
