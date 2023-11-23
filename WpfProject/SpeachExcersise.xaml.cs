using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static System.Net.Mime.MediaTypeNames;

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for SpeachExcersise.xaml
    /// </summary>
    public partial class SpeachExcersise : Page
    {
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();


        public event PropertyChangedEventHandler PropertyChanged;

        public string SelectedSpeedOption
        {
            get

            { return selectedSpeedOption; }
            set
            {
                if (!selectedSpeedOption.Equals(value))
                {
                    selectedSpeedOption = value;
                    OnPropertyChanged(nameof(SelectedSpeedOption));
                    SetSynthesizerRate();
                }
            }
        }
        public string Text { get; private set; }
        public string TypedText { get; set; }
        private string selectedSpeedOption;
        public SpeachExcersise()
        {
            synthesizer.SetOutputToDefaultAudioDevice();
            synthesizer.SelectVoice("Microsoft Frank");
            InitializeComponent();
            DataContext = this;
            selectedSpeedOption = string.Empty;
            Text = "hallo ik spreek nederlands";
        }
        private void Speak(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                synthesizer.Speak(Text);
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
