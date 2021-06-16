using System;
using ChatRoom.Core.Entities;
using ChatRoom.Core.Events;
using ChatRoom.Core.Repositories;

namespace ChatRoom.Core.Commands
{
    public class EnterChatRoomCommand : IChatCommand<EnterChatRoomEvent>
    {
        private readonly IEntityRepository<ChatRoomEntity> _chatRoomRepository;
        private readonly IEventRepository<EnterChatRoomEvent> _eventRepository;
        private readonly IEntityRepository<ParticipantEntity> _participantRepository;

        public EnterChatRoomCommand(IEntityRepository<ChatRoomEntity> chatRoomRepository,
            IEntityRepository<ParticipantEntity> participantRepository,
            IEventRepository<EnterChatRoomEvent> eventRepository)
        {
            _chatRoomRepository = chatRoomRepository;
            _participantRepository = participantRepository;
            _eventRepository = eventRepository;
        }

        public void Execute(EnterChatRoomEvent chatEvent)
        {
            var participant = _participantRepository.GetById(chatEvent.InitiatorParticipantId);
            var chatRoom = _chatRoomRepository.GetById(chatEvent.ChatRoomId);

           participant.HandleEnterChatRoomEvent(chatEvent);
           chatRoom.HandleEnterChatRoomEvent(chatEvent);
            _participantRepository.Update(participant);
            _chatRoomRepository.Update(chatRoom);

            var chatRoomEvent = new EnterChatRoomEvent
            {
                ChatRoomId = chatEvent.ChatRoomId,
                InitiatorParticipantId = chatEvent.InitiatorParticipantId,
                InitiatorParticipantName = chatEvent.InitiatorParticipantName,
                CreatedAt = DateTime.Now
            };

            _eventRepository.Add(chatRoomEvent);
        }
    }
}