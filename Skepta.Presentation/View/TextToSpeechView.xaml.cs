using Skepta.Presentation.ViewModel;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Skepta.Presentation.View
{
    /// <summary>
    /// Interaction logic for TextToSpeechView.xaml
    /// </summary>
    public partial class TextToSpeechView : UserControl
    {
        private TextToSpeechViewModel viewModel;
        public TextToSpeechView()
        {
            InitializeComponent();
            this.DataContextChanged += TextExcersize_DataContextChanged
                ;
        }

        private void TextExcersize_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is TextToSpeechViewModel viewModel)
            {
                this.viewModel = viewModel;
                viewModel.PropertyChanged += ViewModel_PropertyChanged
                    ;
            }
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TextToSpeechViewModel.InputText))
            {
                CompareText();
            }
        }

        private void UpdateUserInputDisplay()
        {
            Dispatcher.Invoke(() =>
            {
                //InputTextBlock.Text = userInput.ToString();
                CompareText();
            });
        }

        private void CompareText()
        {
            InputTextBlock.Inlines.Clear();
            bool isInputCorrect = true;
            var userInput = viewModel.InputText;

            for (int i = 0; i < userInput.Length; i++)
            {
                if (i < viewModel.RandomText.Length)
                {
                    if (userInput[i] == viewModel.RandomText[i])
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

            if (isInputCorrect && userInput.Length == viewModel.RandomText.Length)
            {
                //DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Text typed correctly!", "Success", MessageBoxButtons.OK);
                //if(dialogResult == DialogResult.OK)
                //{
                //NavigationService.Navigate(new resultaat(business));
                //business.StopWatch();
                //business.TimeElapsed();
                //} 
            }
        }
    }
}
