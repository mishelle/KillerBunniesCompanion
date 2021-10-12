using System.Collections.Generic;

namespace KillerBunniesCompanion.ViewModels
{
    public interface IAppController
    {
        void AddTopic(TopicViewModel value);
        IEnumerable<TopicViewModel> GetMatchedTopics(string searchTerm);
    }
}