﻿using System;

namespace ChatRoom.Core.Events
{
    public class EnterChatRoomEvent : IChatRoomEvent
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int InitiatorParticipantId { get; set; }
        public string InitiatorParticipantName { get; set; }
        public int ChatRoomId { get; set; }

        public override string ToString()
        {
            return InitiatorParticipantName + " entered the room";
        }
    }
}