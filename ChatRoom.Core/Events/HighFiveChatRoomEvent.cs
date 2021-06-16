using System;

namespace ChatRoom.Core.Events
{
    public class HighFiveChatRoomEvent : IChatRoomEvent
    {
        public int HighFiveToParticipantId { get; set; }
        public string HighFiveToParticipantName { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int InitiatorParticipantId { get; set; }
        public string InitiatorParticipantName { get; set; }
        public int ChatRoomId { get; set; }

        public override string ToString()
        {
            return InitiatorParticipantName + " high five " + HighFiveToParticipantName;
        }
    }
}