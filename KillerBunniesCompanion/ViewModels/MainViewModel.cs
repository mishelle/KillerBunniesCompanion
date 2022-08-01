using System.Collections.ObjectModel;
using System.Linq;

namespace KillerBunniesCompanion.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            ViewedTopics = new ObservableCollection<TopicViewModel>();
        }

        public ObservableCollection<TopicViewModel> ViewedTopics { get; }
        
        private TopicViewModel _currentTopic;
        public TopicViewModel CurrentTopic
        {
            get => _currentTopic;
            set => SetProperty(ref _currentTopic, value);
        }

        private SearchViewModel _search;
        public SearchViewModel Search
        {
            get => _search;
            set => SetProperty(ref _search, value);
        }
        private bool _isHistoryVisible;
        public bool IsHistoryVisible
        {
            get => _isHistoryVisible;
            set => SetProperty(ref _isHistoryVisible, value);
        }

        internal void AddTopic(TopicViewModel item)
        {
            //remove this item if already there
            var x = ViewedTopics.SingleOrDefault(t => t.Title == item.Title);
            if (x != null) ViewedTopics.Remove(x);

            ViewedTopics.Insert(0, item);
            CurrentTopic = item;
        }

    }
}
