using System;

namespace ChatRoom.Core.Events
{
    public interface IChatRoomEvent
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        int InitiatorParticipantId { get; set; }
        string InitiatorParticipantName { get; set; }
        int ChatRoomId { get; set; }
    }
}