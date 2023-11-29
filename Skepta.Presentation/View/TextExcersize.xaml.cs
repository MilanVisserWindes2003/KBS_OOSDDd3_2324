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
    public static Color purpleText = Color.FromRgb(104, 126, 255);
    public static SolidColorBrush purpleTextBrush = new SolidColorBrush(purpleText);

    public TextExcersize()
    {
        InitializeComponent();
        this.DataContextChanged += TextExcersize_DataContextChanged;
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

    private void CompareText()
    {
        InputTextBlock.Inlines.Clear();
        var userInput = viewModel.InputText;

        for (int i = 0; i < userInput.Length; i++)
        {
            if (i < viewModel.RandomText.Length)
            {
                if (userInput[i] == viewModel.RandomText[i])
                {
                    InputTextBlock.Inlines.Add(new Run(userInput[i].ToString()) { Foreground = purpleTextBrush });
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
                }
            }
        }
    }
}
