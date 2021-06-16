using System;
using ChatRoom.Core.Entities;
using ChatRoom.Core.Events;
using NUnit.Framework;

namespace ChatRoom.Core.Testing.Entities
{
    [TestFixture]
    public class ParticipantEntityFixture
    {
        [Test]
        public void Test_HandleEnterChatRoomEvent_IsSuccesfull()
        {
            // arrange
            var participant = new ParticipantEntity
            {
                Id = 1,
                ChatRoomId = 0,
                Name = "Peter"
            };

            var chatEvent = new EnterChatRoomEvent
            {
                InitiatorParticipantId = 1,
                InitiatorParticipantName = "Peter",
                ChatRoomId = 20,
                CreatedAt = new DateTime(2021, 5, 5, 10, 10, 0)
            };

            // act
            participant.HandleEnterChatRoomEvent(chatEvent);

            // assert
            Assert.That(participant.ChatRoomId, Is.EqualTo(chatEvent.ChatRoomId));
        }
    }
}