using System.Collections.ObjectModel;
using KillerBunniesCompanion.DataModels;

namespace KillerBunniesCompanion.ViewModels
{
    public class TopicViewModel :BaseViewModel
    {
        public TopicViewModel()
        {
            References = new ObservableCollection<ReferenceViewModel>();
        }
        
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string _formattedTitle;
        public string FormattedTitle
        {
            get { return _formattedTitle; }
            set { SetProperty(ref _formattedTitle, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _richText;
        public string RichDescription
        {
            get { return _richText; }
            set { _richText = value; }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }


        private TopicTypes _type;
        public TopicTypes Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
        public ObservableCollection<ReferenceViewModel> References { get; private set; }
        
    }
}
