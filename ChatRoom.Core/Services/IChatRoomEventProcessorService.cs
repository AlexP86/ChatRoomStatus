using ChatRoom.Core.Events;

namespace ChatRoom.Core.Services
{
    public interface IChatRoomEventProcessorService<TEvent> where TEvent : IChatRoomEvent
    {
        void Process(TEvent chatEvent);
    }
}