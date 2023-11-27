using Business;
using Skepta.Presentation.ViewModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Skepta.Presentation.View;

public partial class TextExcersize : UserControl
{
    private TextExcersizeViewModel viewModel;

    public TextExcersize()
    {
        InitializeComponent();
        this.DataContextChanged += TextExcersize_DataContextChanged
            ;
    }

    private void TextExcersize_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is TextExcersizeViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.PropertyChanged += ViewModel_PropertyChanged
                ;
        }
    }

    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TextExcersizeViewModel.InputText))
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
