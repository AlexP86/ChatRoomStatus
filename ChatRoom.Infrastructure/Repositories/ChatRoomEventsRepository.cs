using System.Collections.Generic;
using System.Linq;
using ChatRoom.Core.DataStore;
using ChatRoom.Core.Events;
using ChatRoom.Core.Repositories;

namespace ChatRoom.Infrastructure.Repositories
{
    public class ChatRoomEventsRepository<T> : IEventRepository<T> where T : class, IChatRoomEvent
    {
        private readonly IEventsDataStore _dataStore;

        public ChatRoomEventsRepository(IEventsDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IList<T> GetAll()
        {
            return _dataStore.GetData().OfType<T>().ToList();
        }

        public void Add(T chatEvent)
        {
            _dataStore.Add(chatEvent);
        }
    }
}