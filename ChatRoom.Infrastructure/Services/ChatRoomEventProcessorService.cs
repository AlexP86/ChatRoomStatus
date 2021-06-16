using ChatRoom.Core.Commands;
using ChatRoom.Core.Events;
using ChatRoom.Core.Services;

namespace ChatRoom.Infrastructure.Services
{
    public class ChatRoomEventProcessorService<TEvent> : IChatRoomEventProcessorService<TEvent>
        where TEvent : class, IChatRoomEvent
    {
        private readonly IChatCommand<TEvent> _command;

        public ChatRoomEventProcessorService(IChatCommand<TEvent> command)
        {
            _command = command;
        }

        public void Process(TEvent chatEvent)
        {
            _command.Execute(chatEvent);
        }
    }
}