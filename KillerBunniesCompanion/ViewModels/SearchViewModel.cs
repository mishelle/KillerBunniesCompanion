using System.Collections.ObjectModel;

namespace KillerBunniesCompanion.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private IAppController _context;
        public SearchViewModel():this(App.Controller)
        {
        }
        public SearchViewModel(IAppController appController)
        {
            Topics = new ObservableCollection<TopicViewModel>();
            _context = appController;
        }
        public ObservableCollection<TopicViewModel> Topics { get; private set; }

        private bool _isListVisible;
        public bool IsListVisible
        {
            get { return _isListVisible; }
            set { SetProperty(ref _isListVisible, value); }
        }

        private TopicViewModel _selectedTopic;
        public TopicViewModel SelectedTopic
        {
            get { return _selectedTopic; }
            set { 
                SetProperty(ref _selectedTopic, value);
                //call controller to view the topic?
                Topics.Clear();
                SearchTerm = "";

                _context.AddTopic(value);
            }
        }

        private string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                SetProperty(ref _searchTerm, value);

                Topics.Clear();
                if (value.Length == 0) return;

                if (_context == null) return;
                foreach (var topic in _context.GetMatchedTopics(_searchTerm))
                {
                    Topics.Add(topic);
                }
                IsListVisible = true;
            }
        }

        //events for gotfocus, lostfocus, keydown?, listbox select

        internal void SearchBoxLostFocus()
        {
            //todo: only if the list does not have focus
            IsListVisible = false;
        }

        internal void SearchBoxGotFocus()
        {
            //todo: only if the list does not have focus
            IsListVisible = true;
        }
    }
}
