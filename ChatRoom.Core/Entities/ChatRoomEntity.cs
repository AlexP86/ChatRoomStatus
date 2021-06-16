using System.Collections.Generic;
using System.Linq;
using ChatRoom.Core.Events;

namespace ChatRoom.Core.Entities
{
    public class ChatRoomEntity : IEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public IList<int> PaticipantIds { get; set; }

        public void HandleEnterChatRoomEvent(EnterChatRoomEvent chatEvent)
        {
            PaticipantIds.Add(chatEvent.InitiatorParticipantId);
        }

        public void HandleLeaveChatRoomEvent(LeaveChatRoomEvent chatEvent)
        {
            int itemToRemove = PaticipantIds.SingleOrDefault(p => p == chatEvent.Id);

            if (itemToRemove != 0)
            {
                PaticipantIds.Remove(itemToRemove);
            }
        }
    }
}