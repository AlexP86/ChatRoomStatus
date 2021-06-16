using ChatRoom.Core.Events;

namespace ChatRoom.Core.Entities
{
    public class ParticipantEntity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChatRoomId { get; set; }

        public void HandleEnterChatRoomEvent(EnterChatRoomEvent chatEvent)
        {
            ChatRoomId = chatEvent.ChatRoomId;
        }

        public void HandleLeaveChatRoomEvent()
        {
            ChatRoomId = 0;
        }
    }
}