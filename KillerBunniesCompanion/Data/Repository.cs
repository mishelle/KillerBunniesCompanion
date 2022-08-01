using KillerBunniesCompanion.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KillerBunniesCompanion.Data
{
    public class Repository
    {
        private readonly IDataReader _reader;
        private readonly List<Topic> _data;

        public Repository(IDataReader reader, Deck lastDeck)
        {
            _data = new List<Topic>();
            _reader = reader;
            LoadData(lastDeck);
        }

        public Topic GetDefaultTopic()
        {
            var topic = new Topic
            {
                Title = "Welcome",
                Description = "This will contain stuff about how to set up the game",
            };
            return topic;
        }

        public IEnumerable<Topic> GetMatchedTopics(string search, int maxItems)
        {
            search = TrimAndLower(search);
            var results =  _data.Where(t => TrimAndLower(t.Title).Contains(search))
                                .OrderBy(t => TrimAndLower(t.Title).IndexOf(search))
                                .Take(maxItems).ToList();
            var spaceLeft = maxItems - results.Count;
            if (spaceLeft <= 0) return results;

            var moreResults = _data.Where(t => !results.Contains(t) && TrimAndLower(t.Description).Contains(search))
                                    .Take(spaceLeft);
            return results.Concat(moreResults);
            
        }

        internal Topic GetTopicByTitle(string title)
        {
            var search = TrimAndLower(title);
            var results = _data.Where(t => TrimAndLower(t.Title).Equals(search));
            return results.FirstOrDefault();
        }

        internal string MarkupDescription(Topic topic)
        {
            //todo this is case sensitive right now
            var desc = topic.Description;
            var title = topic.Title;
            desc = desc.Replace(title, "@SELF@");

            foreach (var item in _data)
            {
                desc = desc.Replace(item.Title, string.Format("<ref title2='{0}'>{0}</ref>", item.Title));
            }
            desc = desc.Replace("@SELF@", "<self>" + title + "</self>");

            return desc;
        }

        private void LoadData(Deck lastDeck)
        {
            foreach (Deck deck in Enum.GetValues(typeof(Deck)))
            {
                LoadDataFile(deck);
                if (deck == lastDeck) break;
            }
            //todo possibly here add references
        }

        private void LoadDataFile(Deck deck)
        {
            var data = _reader.LoadDeck(deck);

            foreach (var item in data)
            {
                var existing = _data.Find(t => TitlesMatch(t, item));
                //todo: for each item, add links to existing? check description for references

                if (existing == null)
                    _data.Add(item);
                else
                    existing.OverwriteWith(item);
            }
        }

        private static bool TitlesMatch(Topic t, Topic item)
        {
            return TrimAndLower(t.Title).Equals(TrimAndLower(item.Title));
        }

        private static string TrimAndLower(string p)
        {
            return p == null ? "" : p.Trim().ToLowerInvariant();
        }
    }
}
