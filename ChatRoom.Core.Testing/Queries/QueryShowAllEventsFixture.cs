using ChatRoom.Core.Events;
using ChatRoom.Core.Queries;
using ChatRoom.Core.Queries.QueryArgs;
using ChatRoom.Core.Repositories;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ChatRoom.Core.Testing.Queries
{
    [TestFixture]
    public class QueryShowAllEventsFixture
    {
        protected static readonly DateTime EntryTime = new DateTime(2021, 5, 5, 10, 10, 0);

        protected readonly IList<IChatRoomEvent> data = new List<IChatRoomEvent>
        {
            new EnterChatRoomEvent
            {
                Id = 1, InitiatorParticipantId = 1, InitiatorParticipantName = "Peter", ChatRoomId = 20,
                CreatedAt = EntryTime
            },
            new EnterChatRoomEvent
            {
                Id = 2, InitiatorParticipantId = 2, InitiatorParticipantName = "Robert", ChatRoomId = 20,
                CreatedAt = EntryTime.AddSeconds(5)
            },
            new CommentChatRoomEvent
            {
                Id = 3, InitiatorParticipantId = 1, InitiatorParticipantName = "Peter", ChatRoomId = 20,
                Message = "Hi all", CreatedAt = EntryTime.AddMinutes(1)
            },
            new CommentChatRoomEvent
            {
                Id = 4, InitiatorParticipantId = 1, InitiatorParticipantName = "Peter",
                Message = "Who likes basketball ?", ChatRoomId = 20,
                CreatedAt = EntryTime.AddMinutes(1).AddSeconds(10)
            },
            new CommentChatRoomEvent
            {
                Id = 5, InitiatorParticipantId = 2, InitiatorParticipantName = "Robert", Message = "Hey",
                ChatRoomId = 20, CreatedAt = EntryTime.AddMinutes(1).AddSeconds(11)
            },
            new CommentChatRoomEvent
            {
                Id = 6, InitiatorParticipantId = 2, InitiatorParticipantName = "Robert", Message = "I like basketball",
                ChatRoomId = 20,
                CreatedAt = EntryTime.AddMinutes(1).AddSeconds(12)
            },
            new HighFiveChatRoomEvent
            {
                Id = 7, InitiatorParticipantId = 1, InitiatorParticipantName = "Peter", HighFiveToParticipantId = 2,
                ChatRoomId = 20, HighFiveToParticipantName = "Robert",
                CreatedAt = EntryTime.AddMinutes(1).AddSeconds(15)
            }
        };

        [SetUp]
        public void SetUp()
        {
            _entityRepository = A.Fake<IEventRepository<IChatRoomEvent>>();
            A.CallTo(() => _entityRepository.GetAll()).Returns(data);
        }

        private IEventRepository<IChatRoomEvent> _entityRepository;

        [Test]
        public void RunQuery_ChatRoomDoesNotExist_ReturnsEmptyResult()
        {
            // arrange
            var queryStatusByMinute = new QueryShowAllEvents(_entityRepository);
            var expectedResult = string.Empty;

            // act
            var result = queryStatusByMinute.Run(new ShowAllEventsArgs {ChatRoomId = 500});

            // assert
            Assert.That(result.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void RunQuery_ChatRoomExists_ReturnsCorrectResult()
        {
            // arrange
            var queryStatusByMinute = new QueryShowAllEvents(_entityRepository);
            var expectedResult =
                "10:10 AM Peter entered the room\r\n10:10 AM Robert entered the room\r\n10:11 AM Peter commented: Hi all\r\n10:11 AM Peter commented: Who likes basketball ?\r\n10:11 AM Robert commented: Hey\r\n10:11 AM Robert commented: I like basketball\r\n10:11 AM Peter high five Robert\r\n";

            // act
            var result = queryStatusByMinute.Run(new ShowAllEventsArgs { ChatRoomId = 20});

            // assert
            Assert.That(result.ToString(), Is.EqualTo(expectedResult).IgnoreCase);
        }
    }
}