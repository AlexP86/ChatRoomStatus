using System.Linq;
using System.Text;
using ChatRoom.Core.Events;
using ChatRoom.Core.Queries.QueryArgs;
using ChatRoom.Core.Repositories;

namespace ChatRoom.Core.Queries
{
    public class QueryShowAllEvents : IQuery<ShowAllEventsArgs>
    {
        private readonly IEventRepository<IChatRoomEvent> _eventRepository;

        public QueryShowAllEvents(IEventRepository<IChatRoomEvent> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public object Run(ShowAllEventsArgs args)
        {
            var report = new StringBuilder();
            var chatEvents = _eventRepository.GetAll()
                .Where(i => i.ChatRoomId == args.ChatRoomId)
                .OrderByDescending(i => i.CreatedAt.Date)
                .ThenBy(i => i.CreatedAt.TimeOfDay);

            foreach (IChatRoomEvent chatEvent in chatEvents)
            {
                report.AppendLine(chatEvent.CreatedAt.ToString("hh:mm tt") + " " + chatEvent);
            }

            return report.ToString();
        }
    }
}