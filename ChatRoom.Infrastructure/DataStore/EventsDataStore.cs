using System;
using System.Collections.Generic;
using System.Linq;
using ChatRoom.Core.DataStore;
using ChatRoom.Core.Events;

namespace ChatRoom.Infrastructure.DataStore
{
    public class EventsDataStore : IEventsDataStore
    {
        private static readonly DateTime EntryTime = DateTime.Now.AddHours(-5);
        private static int _nextId = 1000;

        // this could be a dictionary, but it is data mock so never mind
        private readonly IList<object> _data = new List<object>
        {
            new EnterChatRoomEvent {Id = 1, InitiatorParticipantId = 1 , InitiatorParticipantName = "Peter", ChatRoomId = 20, CreatedAt = EntryTime},
            new EnterChatRoomEvent
                {Id = 2, InitiatorParticipantId = 2, InitiatorParticipantName = "Robert", ChatRoomId = 20, CreatedAt = EntryTime.AddSeconds(5)},
            new CommentChatRoomEvent
                {Id = 3, InitiatorParticipantId = 1, InitiatorParticipantName = "Peter", ChatRoomId = 20, Message = "Hi all", CreatedAt = EntryTime.AddMinutes(1)},
            new CommentChatRoomEvent
            {
                Id = 4, InitiatorParticipantId = 1, InitiatorParticipantName = "Peter", Message = "Who likes basketball ?", ChatRoomId = 20,
                CreatedAt = EntryTime.AddMinutes(1).AddSeconds(10)
            },
            new CommentChatRoomEvent
            {
                Id = 5, InitiatorParticipantId = 2, InitiatorParticipantName = "Robert", Message = "Hey", ChatRoomId = 20,  CreatedAt = EntryTime.AddMinutes(1).AddSeconds(11)
            },
            new CommentChatRoomEvent
            {
                Id = 6, InitiatorParticipantId = 2, InitiatorParticipantName = "Robert", Message = "I like basketball", ChatRoomId = 20,
                CreatedAt = EntryTime.AddMinutes(1).AddSeconds(12)
            },
            new HighFiveChatRoomEvent
            {
                Id = 7, InitiatorParticipantId = 1, InitiatorParticipantName = "Peter", HighFiveToParticipantId = 2, ChatRoomId = 20, HighFiveToParticipantName = "Robert",
                CreatedAt = EntryTime.AddMinutes(1).AddSeconds(15)
            },
            new EnterChatRoomEvent
                {Id = 8, InitiatorParticipantId = 5, InitiatorParticipantName = "Lisa", ChatRoomId = 20, CreatedAt = EntryTime.AddHours(1)},
            new LeaveChatRoomEvent
                {Id = 9, InitiatorParticipantId = 2, InitiatorParticipantName = "Robert", ChatRoomId = 20, CreatedAt = EntryTime.AddHours(1)},
            new CommentChatRoomEvent
            {
                Id = 10, InitiatorParticipantId = 5, InitiatorParticipantName = "Lisa", Message = "Does anyone like football", ChatRoomId = 20,
                CreatedAt = EntryTime.AddHours(1).AddSeconds(2)
            },
            new CommentChatRoomEvent
            {
                Id = 11, InitiatorParticipantId = 1, InitiatorParticipantName = "Peter", Message = "No I prefer basketball", ChatRoomId = 20,
                CreatedAt = EntryTime.AddHours(1).AddSeconds(5)
            },
            new LeaveChatRoomEvent
                {Id = 12, InitiatorParticipantId = 5, InitiatorParticipantName = "Lisa", ChatRoomId = 20, CreatedAt = EntryTime.AddHours(1).AddSeconds(7)},
             new LeaveChatRoomEvent
                {Id = 50, InitiatorParticipantId = 1, InitiatorParticipantName = "Peter", ChatRoomId = 20, CreatedAt = EntryTime.AddHours(1).AddMinutes(7)},
            new EnterChatRoomEvent
                {Id = 13, InitiatorParticipantId = 4, InitiatorParticipantName = "Alice", ChatRoomId = 21, CreatedAt = EntryTime.AddMinutes(2)},
            new CommentChatRoomEvent
            {
                Id = 14, InitiatorParticipantId = 4, InitiatorParticipantName = "Alice", ChatRoomId = 21, Message = "Hi all",
                CreatedAt = EntryTime.AddMinutes(2)
            },
            new EnterChatRoomEvent
                {Id = 15, InitiatorParticipantId = 3, InitiatorParticipantName = "Mike", ChatRoomId = 21, CreatedAt = EntryTime.AddMinutes(5)},
            new CommentChatRoomEvent
            {
                Id = 16, InitiatorParticipantId = 3, InitiatorParticipantName = "Mike", ChatRoomId = 21, Message = "Are you still here ?",
                CreatedAt = EntryTime.AddHours(1)
            },
            new CommentChatRoomEvent
            {
                Id = 17, InitiatorParticipantId = 4, InitiatorParticipantName = "Alice", ChatRoomId = 21, Message = "Yes",
                CreatedAt = EntryTime.AddHours(1).AddSeconds(1)
            },
            new CommentChatRoomEvent
            {
                Id = 18, InitiatorParticipantId = 3, InitiatorParticipantName = "Mike", ChatRoomId = 21, Message = "Do you like math ?",
                CreatedAt = EntryTime.AddHours(1).AddSeconds(2)
            },
            new CommentChatRoomEvent
            {
                Id = 19, InitiatorParticipantId = 4, InitiatorParticipantName = "Alice", ChatRoomId = 21, Message = "I like math",
                CreatedAt = EntryTime.AddHours(1).AddSeconds(4)
            },
            new HighFiveChatRoomEvent
            {
                Id = 20, InitiatorParticipantId = 3, InitiatorParticipantName = "Mike", HighFiveToParticipantId = 4, ChatRoomId = 21, HighFiveToParticipantName = "Alice",
                CreatedAt = EntryTime.AddHours(1).AddSeconds(8)
            },
             new LeaveChatRoomEvent
                {Id = 21, InitiatorParticipantId = 3, InitiatorParticipantName = "Mike", ChatRoomId = 21, CreatedAt = EntryTime.AddHours(1).AddSeconds(20)},
              new LeaveChatRoomEvent
                {Id = 22, InitiatorParticipantId = 4, InitiatorParticipantName = "Alice", ChatRoomId = 21, CreatedAt = EntryTime.AddHours(1).AddSeconds(70)}
        };

        public IEnumerable<object> GetData()
        {
            return _data;
        }

        public void Add(object item)
        {
            var chatEvent = item as IChatRoomEvent;
            chatEvent.Id = _nextId++;
            _data.Add(chatEvent);
        }

        public void Remove(int id)
        {
            var item = _data.Single(i => (i as IChatRoomEvent).Id == id);
            _data.Remove(item);
        }
    }
}