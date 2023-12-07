using Skepta.Business;
using Skepta.Presentation.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Skepta.Presentation.ViewModel;

public class TextDifficultySelectViewModel : ViewModelBase
{
    private readonly SkeptaModel model;
    private Dictionary<string, int> selected;
    private string currenSelection = string.Empty;

    public TextDifficultySelectViewModel(SkeptaModel model)
    {
        this.model = model;
        selected = new Dictionary<string, int>
        {
            {"A1", 5 },
            {"A2", 5 },
            {"B1", 5 },
            {"B2", 5 },
            {"C1", 5 },
            {"C2", 5 },
        };
        
    }

    public ICommand Select => new BaseCommand<string>((param) => SelectCmd(param));

    public int[] Selected { get => selected.Values.ToArray();}

    private void SelectCmd(string difficulty)
    {
        if (!string.IsNullOrEmpty(currenSelection))
        {
            selected[currenSelection] = 5;
        }
        currenSelection = difficulty;
        selected[currenSelection] = 15;
        NotifyPropertyChanged(nameof(Selected));    

        model.TextDifficulty = difficulty;
        RequestPage = PageId.TextLength;
    }

    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isSelected = (bool)value;
            return isSelected ? Brushes.Black : Brushes.GhostWhite;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
