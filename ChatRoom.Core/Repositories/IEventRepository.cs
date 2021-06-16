using System.Collections.Generic;
using ChatRoom.Core.Events;

namespace ChatRoom.Core.Repositories
{
    public interface IEventRepository<T> where T : IChatRoomEvent
    {
        IList<T> GetAll();
        void Add(T chatEvent);
    }
}