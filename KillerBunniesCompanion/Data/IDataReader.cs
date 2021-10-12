using KillerBunniesCompanion.DataModels;
using System.Collections.Generic;

namespace KillerBunniesCompanion.Data
{
    public interface IDataReader
    {
        IEnumerable<Topic> LoadDeck(Decks deck);
    }
}
