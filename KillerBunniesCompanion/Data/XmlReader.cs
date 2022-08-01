using KillerBunniesCompanion.DataModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.ApplicationModel;

namespace KillerBunniesCompanion.Data
{
    public class XmlReader : IDataReader
    {
        private readonly string _rootPath = Package.Current.InstalledLocation.Path;
        public IEnumerable<Topic> LoadDeck(Deck deck)
        {
            var path = $"Data/Files/{deck}.xml";
            var xmlPath = Path.Combine(_rootPath, path);
            
            var document = ReadDocument(path);
            if (document == null) return new List<Topic>();

            return from query in document.Descendants("topic")
                   select new Topic
                   {
                       Title = (string)query.Element("title"),
                       Description = (string)query.Element("description"),
                       Type = GetType((string)query.Attribute("type")),
                       Deck = deck
                   };
        }
        
        private static XDocument ReadDocument(string xmlPath)
        {
            try
            {
                return XDocument.Load(xmlPath);
            }
            catch
            {
                return null;
            }
        }
        private static TopicType GetType(string p)
        {
            switch (p)
            {
                case "gameplay":
                    return TopicType.GamePlay;
                case "cardtype":
                    return TopicType.CardType;
                default:
                    return TopicType.Card;
            }
        }
    }
}
