namespace KillerBunniesCompanion.ViewModels
{
    public class ReferenceViewModel : BaseViewModel
    {
        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
        private int _linkId;
        public int LinkId
        {
            get => _linkId;
            set => SetProperty(ref _linkId, value);
        }
    }
}
