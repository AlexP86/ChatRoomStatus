using System.Collections.Generic;
using ChatRoom.Core.Events;

namespace ChatRoom.Core.DataStore
{
    public interface IEventsDataStore
    {
        IEnumerable<object> GetData();
        void Add(object item);
        void Remove(int id);
    }
}