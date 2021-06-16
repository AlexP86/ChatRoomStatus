using ChatRoom.Core.Entities;
using ChatRoom.Core.Events;
using ChatRoom.Core.Repositories;
using System;

namespace ChatRoom.Core.Commands
{
    public class LeaveChatRoomCommand : IChatCommand<LeaveChatRoomEvent>
    {
        private readonly IEntityRepository<ChatRoomEntity> _chatRoomRepository;
        private readonly IEventRepository<LeaveChatRoomEvent> _eventRepository;
        private readonly IEntityRepository<ParticipantEntity> _participantRepository;

        public LeaveChatRoomCommand(IEntityRepository<ChatRoomEntity> chatRoomRepository,
           IEntityRepository<ParticipantEntity> participantRepository,
           IEventRepository<LeaveChatRoomEvent> eventRepository)
        {
            _chatRoomRepository = chatRoomRepository;
            _participantRepository = participantRepository;
            _eventRepository = eventRepository;
        }

        public void Execute(LeaveChatRoomEvent chatEvent)
        {
            var participant = _participantRepository.GetById(chatEvent.InitiatorParticipantId);
            var chatRoom = _chatRoomRepository.GetById(chatEvent.ChatRoomId);

            participant.HandleLeaveChatRoomEvent();
            chatRoom.HandleLeaveChatRoomEvent(chatEvent);
            _participantRepository.Update(participant);
            _chatRoomRepository.Update(chatRoom);

            var chatRoomEvent = new LeaveChatRoomEvent
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
