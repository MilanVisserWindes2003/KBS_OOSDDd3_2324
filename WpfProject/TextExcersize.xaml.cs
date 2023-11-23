using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfProject
{
    public partial class TextExcersize : Page
    {
        Business.Business business;
        private StringBuilder userInput = new StringBuilder();
        private int aantalTekens;
        private int aantalWoorden;
        
        public TextExcersize(Business.Business business)
        {
            
            this.business.timer.Interval = TimeSpan.FromSeconds(1);
            this.business.timer.Tick += Timer_Tick;
            InitializeComponent();
            this.business = business;
            this.DataContext = business;
            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
            //business.waitToStartTimer();
            this.Focus();
        }
        public void Timer_Tick(object sender, EventArgs e)
        {
            business.AddTimerTick();
            
        }
        public void MainWindow_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                return;
            }

            if (e.Key == Key.Back)
            {
                if (userInput.Length > 0)
                {
                    userInput.Length -= 1;
                    UpdateUserInputDisplay();
                }
            }
            else if (e.Key == Key.Space)
            {
                userInput.Append(" ");
                aantalWoorden++;
                UpdateUserInputDisplay();
            }
            else if (business.IsPrintableKey(e.Key))
            {
                string input = business.GetPrintableCharacter(e.Key, Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift));
                userInput.Append(input);
                UpdateUserInputDisplay();
            }
            else if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CompareText();
            }
            e.Handled = true;
        }

        private void UpdateUserInputDisplay()
        {
            Dispatcher.Invoke(() =>
            {
                InputTextBlock.Text = userInput.ToString();
                CompareText();
            });
        }

        private void CompareText()
        {
            InputTextBlock.Inlines.Clear();

            bool isInputCorrect = true;

            for (int i = 0; i < userInput.Length; i++)
            {
                if (i < business.RandomText.Length)
                {
                    if (userInput[i] == business.RandomText[i])
                    {
                        InputTextBlock.Inlines.Add(new Run(userInput[i].ToString()) { Foreground = Brushes.Green });
                    }
                    else
                    {
                        if (userInput[i] == ' ')
                        {
                            InputTextBlock.Inlines.Add(new Run("_") { Foreground = Brushes.Red });
                        }
                        else
                        {
                            InputTextBlock.Inlines.Add(new Run(userInput[i].ToString()) { Foreground = Brushes.Red });
                        }

                        isInputCorrect = false;
                    }
                }
                else
                {
                    InputTextBlock.Inlines.Add(new Run(userInput[i].ToString()) { Foreground = Brushes.Red });
                    isInputCorrect = false;
                }
            }

            if (isInputCorrect && userInput.Length == business.RandomText.Length)
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Text typed correctly!", "Success", MessageBoxButtons.OK);
                if(dialogResult == DialogResult.OK)
                {
                    NavigationService.Navigate(new resultaat(business));
                    //business.StopWatch();
                    //business.TimeElapsed();
                } 
            }
        }
    }
}
