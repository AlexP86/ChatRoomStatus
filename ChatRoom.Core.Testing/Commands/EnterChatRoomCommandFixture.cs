using System.Collections.Generic;
using System.Linq;
using ChatRoom.Core.Commands;
using ChatRoom.Core.Entities;
using ChatRoom.Core.Events;
using ChatRoom.Core.Repositories;
using FakeItEasy;
using NUnit.Framework;

namespace ChatRoom.Core.Testing.Commands
{
    [TestFixture]
    public class EnterChatRoomCommandFixture
    {
        [Test]
        public void ExecuteCommand_ChatRoomExists_ReturnsCorrectResult()
        {
            // arrange
            var chatRoomRepository = A.Fake<IEntityRepository<ChatRoomEntity>>();
            var participantRepository = A.Fake<IEntityRepository<ParticipantEntity>>();
            var eventRepository = A.Fake<IEventRepository<EnterChatRoomEvent>>();

            var chatRoom = new ChatRoomEntity
            {
                Id = 20,
                Name = "Sports",
                PaticipantIds = new List<int>()
            };

            var participant = new ParticipantEntity
            {
                Id = 1,
                Name = "Peter",
                ChatRoomId = 0
            };

            A.CallTo(() => chatRoomRepository.GetById(A<int>._)).Returns(chatRoom);
            A.CallTo(() => participantRepository.GetById(A<int>._)).Returns(participant);

            var enterChatRoomCommand =
                new EnterChatRoomCommand(chatRoomRepository, participantRepository, eventRepository);

            // act
            var chatEvent = new EnterChatRoomEvent
            {
                ChatRoomId = chatRoom.Id,
                InitiatorParticipantId = participant.Id
            };

            enterChatRoomCommand.Execute(chatEvent);

            // assert
            Assert.That(participant.ChatRoomId, Is.EqualTo(chatRoom.Id));
            Assert.That(chatRoom.PaticipantIds.Single(), Is.EqualTo(participant.Id));
            A.CallTo(() => chatRoomRepository.Update(chatRoom)).MustHaveHappenedOnceExactly();
            A.CallTo(() => participantRepository.Update(participant)).MustHaveHappenedOnceExactly();
        }
    }
}