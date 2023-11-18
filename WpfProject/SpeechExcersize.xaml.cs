using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Speech.Synthesis;
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
    /// Interaction logic for SpeechExcersize.xaml
    /// </summary>
    public partial class SpeechExcersize : Page
    {
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        public string SelectedSpeedOption
        {
            get

            { return selectedSpeedOption; }
            set
            {
                if (!selectedSpeedOption.Equals(value))
                {
                    selectedSpeedOption = value;
                   
                    SetSynthesizerRate();
                }
            }
        }
        public string Text { get; private set; }
        public string TypedText { get; set; }
        private string selectedSpeedOption;
        public SpeechExcersize(Business.Business business)
        {
            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.SelectVoice("Microsoft Frank");
            InitializeComponent();
            DataContext = this;
            selectedSpeedOption = string.Empty;
            Text = business.RandomText;
        }
        private void Speak(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                synthesizer.Speak(Text);
            }
        }
       
        private void SetSynthesizerRate()
        {
            if (int.TryParse(SelectedSpeedOption, out int rate))
            {
                synthesizer.Rate = rate;
            }
        }
    }
}
