using Buisness.TTS;
using Skepta.Business;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    public class TypeWindowModel : ViewModelBase
    {

        private readonly SkeptaModel model;
        private string typedText;
        private TextToSpeechConverter TTS;
        private ICommand send;
        public TypeWindowModel(SkeptaModel model)
        {
            this.model = model;
            TTS = new TextToSpeechConverter();
            typedText = string.Empty;
            Story = "Story";
            
        }
        public string TypedText { get => typedText; set => typedText = value; }
        public string Story { get; set; }
        public bool isStoryVisible = false;
       


        private void checkTextsCommand(string story, string typedText)
        {
            Key key = Key.None;
        }
    }
}
