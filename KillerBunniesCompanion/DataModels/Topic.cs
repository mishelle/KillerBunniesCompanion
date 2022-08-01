using System.Collections.Generic;

namespace KillerBunniesCompanion.DataModels
{
    public class Topic
    {
        public Topic()
        {
            //Descriptions = new List<Description>();
            RelatedIds = new List<int>();
            Notes = "";
        }
        public int Id { get; set; }
        public Deck Deck { get; set; }
        public TopicType Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> RelatedIds { get; set; }
        public string Notes { get; set; }

        public Topic OverwriteWith(Topic item)
        {
            Deck = item.Deck;
            Description = item.Description;
            Notes += item.Notes;
            RelatedIds.AddRange(item.RelatedIds);
            return this;
        }
    }
}
