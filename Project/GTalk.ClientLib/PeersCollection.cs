using System.Collections.ObjectModel;

namespace GTalk.Client
{
    internal class PeersCollection : KeyedCollection<string, string>
    {
        protected override string GetKeyForItem(string item)
        {
            return item;
        }
    }
}