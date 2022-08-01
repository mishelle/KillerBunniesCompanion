using KillerBunniesCompanion.Data;
using KillerBunniesCompanion.ViewModels;
using System.Collections.Generic;

namespace KillerBunniesCompanion
{
    public class AppController :IAppController
    {
        private readonly Repository _repository;
        private MainViewModel _mainVm;

        public AppController()
        {
            var reader = new XmlReader();
            _repository = new Repository(reader, DataModels.Deck.Pumpkin);
        }

        public MainViewModel GetMainViewModel()
        {
            _mainVm = new MainViewModel {Search = new SearchViewModel {SearchTerm = ""}};

            _mainVm.AddTopic(MakeTopicVM(_repository.GetDefaultTopic()));
            return _mainVm;
        }

        internal TopicViewModel MakeTopicVM(DataModels.Topic topic)
        {
            var topicVM = new TopicViewModel();
            topicVM.Title = topic.Title;
            topicVM.RichDescription = _repository.MarkupDescription(topic);
            return topicVM;
        }
        internal TopicViewModel MakeTopicVM(DataModels.Topic topic, string searchTerm)
        {
            var topicVM = MakeTopicVM(topic);
            topicVM.FormattedTitle = AddBoldToTitle(topic.Title, searchTerm);
            return topicVM;
        }

        private static string AddBoldToTitle(string title, string searchTerm)
        {
            if (searchTerm.Length == 0) return title;
            var index = title.ToLowerInvariant().IndexOf(searchTerm.ToLowerInvariant());
            if (index < 0) return title;
            return title.Insert(index + searchTerm.Length, "</b>").Insert(index, "<b>");
        }

        public void AddTopic(TopicViewModel value)
        {
            _mainVm.AddTopic(value);
        }

        public IEnumerable<TopicViewModel> GetMatchedTopics(string searchTerm)
        {
            foreach (var topic in _repository.GetMatchedTopics(searchTerm, 10))
                yield return MakeTopicVM(topic, searchTerm);
        }

        internal void GoToTopic(string title)
        {
            var result = _repository.GetTopicByTitle(title);
            if (result != null) 
                AddTopic(MakeTopicVM(result));
        }
    }
}
