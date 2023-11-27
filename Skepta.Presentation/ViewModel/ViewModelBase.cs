using Org.BouncyCastle.Asn1.Mozilla;
using Skepta.Business.Util;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel;

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

    public virtual void OpenPage(){ }

    public virtual void KeyPressed(Key key){ }
}


