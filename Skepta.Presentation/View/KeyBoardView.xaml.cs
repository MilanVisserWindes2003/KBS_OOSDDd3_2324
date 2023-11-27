using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Skepta.Presentation.View;

/// <summary>
/// Interaction logic for KeyBoardView.xaml
/// </summary>
public partial class KeyBoardView : UserControl
{
    public KeyBoardView()
    {
        InitializeComponent();
        this.DataContext = this;
        Result = "";
        ShowNumericKeyboard = false;
    }

    private bool _showNumericKeyboard;
    public bool ShowNumericKeyboard
    {
        get { return _showNumericKeyboard; }
        set { _showNumericKeyboard = value; this.OnPropertyChanged("ShowNumericKeyboard"); }
    }

    private string _result;
    public string Result
    {
        get { return _result; }
        private set { _result = value; this.OnPropertyChanged("Result"); }
    }

    private void button_Click(object sender, RoutedEventArgs e)
    {
        Button button = sender as Button;
        if (button != null)
        {
            switch (button.CommandParameter.ToString())
            {
                case "LSHIFT":
                    Regex upperCaseRegex = new Regex("[A-Z]");
                    Regex lowerCaseRegex = new Regex("[a-z]");
                    Button btn;
                    foreach (UIElement elem in AlfaKeyboard.Children) //iterate the main grid
                    {
                        Grid grid = elem as Grid;
                        if (grid != null)
                        {
                            foreach (UIElement uiElement in grid.Children)  //iterate the single rows
                            {
                                btn = uiElement as Button;
                                if (btn != null) // if button contains only 1 character
                                {
                                    if (btn.Content.ToString().Length == 1)
                                    {
                                        if (upperCaseRegex.Match(btn.Content.ToString()).Success) // if the char is a letter and uppercase
                                            btn.Content = btn.Content.ToString().ToLower();
                                        else if (lowerCaseRegex.Match(button.Content.ToString()).Success) // if the char is a letter and lower case
                                            btn.Content = btn.Content.ToString().ToUpper();
                                    }

                                }
                            }
                        }
                    }
                    break;

                case "ALT":
                case "CTRL":
                    break;

                case "RETURN":
                    break;

                case "BACK":
                    if (Result.Length > 0)
                    {
                        Result = Result.Remove(Result.Length - 1);
                    }
                    break;

                default:
                    Result += button.Content.ToString();
                    break;
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
            this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }
}
