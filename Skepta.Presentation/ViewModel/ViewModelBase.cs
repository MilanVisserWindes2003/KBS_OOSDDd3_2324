using Skepta.Business.Util;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel;

//Implements ObservableObject
public class ViewModelBase : ObservableObject
{
    private PageId page;
    public bool RequestNext
    {
        set => NotifyPropertyChanged(nameof(RequestNext));
    }

    public bool RequestPrev
    {
        set => NotifyPropertyChanged(nameof(RequestPrev));
    }

    public PageId RequestPage
    {
        get => page;
        set
        {
            page = value;
            NotifyPropertyChanged(nameof(RequestPage));
        }
    }

    //When the page is opened, virtual to make it able to change
    public virtual void OpenPage(){ }

    //When key is pressed, virtual to make it able to change
    public virtual void KeyPressed(Key key){ }
}


