using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerBunniesCompanion.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            ViewedTopics = new ObservableCollection<TopicViewModel>();
        }

        public ObservableCollection<TopicViewModel> ViewedTopics { get; private set; }
        
        private TopicViewModel _currentTopic;
        public TopicViewModel CurrentTopic
        {
            get { return _currentTopic; }
            set 
            { 
                SetProperty(ref _currentTopic, value);
            }
        }

        private SearchViewModel _search;
        public SearchViewModel Search
        {
            get { return _search; }
            set
            {
                SetProperty(ref _search, value);
            }
        }
        private bool _isHistoryVisible;
        public bool IsHistoryVisible
        {
            get { return _isHistoryVisible; }
            set
            {
                SetProperty(ref _isHistoryVisible, value);
            }
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
