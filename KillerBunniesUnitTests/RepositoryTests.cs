using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using KillerBunniesCompanion.Data;
using System.Xml.Linq;
using KillerBunniesCompanion.DataModels;

namespace KillerBunniesUnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        private MyDataReader _reader;
        [TestInitialize]
        public void Setup()
        {
            _reader = new MyDataReader();
        }
        //given a deck, make sure data from subsequent deck is not loaded; make sure later version of topic is retrieved; test search matching, order of results
        [TestMethod]
        public void TestOneDeckLoadedTopicReturned()
        {
            var topic = new Topic { Deck = Decks.Red, Title = "Test 1" };
            _reader.AddResult(topic); 

            var repos = new Repository(_reader, Decks.Red);
            var topics = repos.GetMatchedTopics("test", 2);
            CheckResults(topics, new List<Topic>{topic});
        }

        private void CheckResults(IEnumerable<Topic> actual, List<Topic> expected)
        {
            Assert.AreEqual(expected.Count(), actual.Count());
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestUnusedDeckIsNotLoaded()
        {
            var blueTopic = new Topic { Deck = Decks.Blue, Title = "Test 1" };
            var redTopic = new Topic { Deck = Decks.Red, Title = "Test 2" };
            _reader.AddResult(blueTopic);
            _reader.AddResult(redTopic);

            var repos = new Repository(_reader, Decks.Blue);
            var topics = repos.GetMatchedTopics("test", 2);
            CheckResults(topics, new List<Topic> { blueTopic });
        }
        [TestMethod]
        public void TestTwoDecksLoaded()
        {
            var blueTopic = new Topic { Deck = Decks.Blue, Title = "Test 1" };
            var redTopic = new Topic { Deck = Decks.Red, Title = "Test 2" };
            _reader.AddResult(blueTopic);
            _reader.AddResult(redTopic);

            var repos = new Repository(_reader, Decks.Red);
            var topics = repos.GetMatchedTopics("test", 2);
            CheckResults(topics, new List<Topic> { blueTopic, redTopic });
        }
        [TestMethod]
        public void TestSecondTopicOverwritesFirst()
        {
            var blueTopic = new Topic { Deck = Decks.Blue, Title = "Test", Description = "test 1" };
            var redTopic = new Topic { Deck = Decks.Red, Title = "Test", Description = "test 2" };
            _reader.AddResult(blueTopic);
            _reader.AddResult(redTopic);
            
            var repos = new Repository(_reader, Decks.Red);
            var topics = repos.GetMatchedTopics("test", 2);
            CheckResults(topics, new List<Topic> { blueTopic.OverwriteWith(redTopic) });
        }
        [TestMethod]
        public void TestOrderOfResults()
        {
            var topic1 = new Topic { Deck = Decks.Blue, Title = "First Test", Description = "test 1" };
            var topic2 = new Topic { Deck = Decks.Blue, Title = "Test 2", Description = "test 1" };
            var topic3 = new Topic { Deck = Decks.Blue, Title = "3rd Test", Description = "test 2" };
            _reader.AddResult(topic1);
            _reader.AddResult(topic2);
            _reader.AddResult(topic3);
            
             var repos = new Repository(_reader, Decks.Red);
             var topics = repos.GetMatchedTopics("test", 5);
             CheckResults(topics, new List<Topic> { topic2, topic3, topic1 });
        }
        [TestMethod]
        public void TestLimitOfResults()
        {
            var topic1 = new Topic { Deck = Decks.Blue, Title = "First Test", Description = "test 1" };
            var topic2 = new Topic { Deck = Decks.Blue, Title = "Test 2", Description = "test 1" };
            var topic3 = new Topic { Deck = Decks.Blue, Title = "3rd Test", Description = "test 2" };
            _reader.AddResult(topic1);
            _reader.AddResult(topic2);
            _reader.AddResult(topic3);

            var repos = new Repository(_reader, Decks.Red);
            var topics = repos.GetMatchedTopics("test", 2);
            CheckResults(topics, new List<Topic> { topic2, topic3 });
        }

        [TestMethod]
        public void TestCheckDescriptionsAsWell()
        {
            var topic1 = new Topic { Deck = Decks.Blue, Title = "First Test", Description = "test 1" };
            var topic2 = new Topic { Deck = Decks.Blue, Title = "Test 2", Description = "test 3" };
            var topic3 = new Topic { Deck = Decks.Blue, Title = "3rd Test", Description = "test 2" };
            _reader.AddResult(topic1);
            _reader.AddResult(topic2);
            _reader.AddResult(topic3);

            var repos = new Repository(_reader, Decks.Red);
            var topics = repos.GetMatchedTopics("3", 5);
            CheckResults(topics, new List<Topic> { topic3, topic2 });
        }
    }

    public class MyDataReader : IDataReader
    {
        Dictionary<Decks, List<Topic>> _results;
        public MyDataReader()
        {
            _results = new Dictionary<Decks, List<Topic>>();
        }
        public IEnumerable<Topic> LoadDeck(Decks deck)
        {
            if (_results.ContainsKey(deck))
                return _results[deck];
            else
                return new List<Topic>();
        }

        internal void AddResult(Topic topic)
        {
            var deck = topic.Deck;
            if (_results.ContainsKey(deck))
                _results[deck].Add(topic);
            else
                _results.Add(deck, new List<Topic> { topic });
        }
    }
}
