using System.Windows;
using System.Speech.Synthesis;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for Speech_Window.xaml
    /// </summary>

    public partial class Speech_Window : Window
    {


        

        public string Text { get; private set; }
        public string TypedText { get; set; }
        public Speech_Window()
        {
           
            InitializeComponent();
            DataContext = this;
            selectedSpeedOption = 0;
            Text = "hallo ik spreek nederlands";
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
            synthesizer.Rate = (int)selectedSpeedOption;
        }
    }
}
