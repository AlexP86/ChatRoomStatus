using System;
using ChatRoom.Core.Events;
using NUnit.Framework;

namespace ChatRoom.Core.Testing.Events
{
    [TestFixture]
    public class EnterChatRoomEventFixture
    {
        [Test]
        public void Test_ToString_ReturnsCorrectResult()
        {
            // arrange
            var chatEvent = new EnterChatRoomEvent
            {
                InitiatorParticipantId = 1,
                InitiatorParticipantName = "Peter",
                ChatRoomId = 20,
                CreatedAt = new DateTime(2021, 5, 5, 10, 10, 0)
            };

            // act
            var result = chatEvent.ToString();

            // assert
            Assert.That(result, Is.EqualTo("Peter entered the room"));
        }
    }
}