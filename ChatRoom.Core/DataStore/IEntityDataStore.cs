using System.Collections.Generic;

namespace ChatRoom.Core.DataStore
{
    public interface IEntityDataStore
    {
        IEnumerable<object> GetData();
        void Add(object item);
        void Remove(int id);
    }
}