using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Models
{
    public class ChatRoomLobbyModel
    {
        [Required] public string ParticipantId { get; set; }

        [Required] public string ChatRoomId { get; set; }
    }
}