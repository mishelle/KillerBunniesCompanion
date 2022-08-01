using System.Collections.ObjectModel;
using KillerBunniesCompanion.DataModels;

namespace KillerBunniesCompanion.ViewModels
{
    public class TopicViewModel : BaseViewModel
    {
        public TopicViewModel()
        {
            References = new ObservableCollection<ReferenceViewModel>();
        }
        
        public ObservableCollection<ReferenceViewModel> References { get; }

        public string RichDescription { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _formattedTitle;
        public string FormattedTitle
        {
            get => _formattedTitle;
            set => SetProperty(ref _formattedTitle, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _notes;
        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        private TopicType _type;
        public TopicType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }
    }
}
