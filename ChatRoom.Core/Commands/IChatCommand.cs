using ChatRoom.Core.Events;

namespace ChatRoom.Core.Commands
{
    public interface IChatCommand<TEvent> where TEvent : IChatRoomEvent
    {
        void Execute(TEvent chatEvent);
    }
}