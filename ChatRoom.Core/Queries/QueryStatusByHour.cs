using System.Linq;
using System.Text;
using ChatRoom.Core.Events;
using ChatRoom.Core.Queries.QueryArgs;
using ChatRoom.Core.Repositories;

namespace ChatRoom.Core.Queries
{
    public class QueryStatusByHour : IQuery<StatusByHourArgs>
    {
        private readonly IEventRepository<IChatRoomEvent> _eventRepository;

        public QueryStatusByHour(IEventRepository<IChatRoomEvent> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public object Run(StatusByHourArgs args)
        {
            var report = new StringBuilder();
            var groups = _eventRepository.GetAll()
                .Where(i => i.ChatRoomId == args.ChatRoomId)
                .OrderByDescending(i => i.CreatedAt.Date)
                .ThenBy(i => i.CreatedAt.TimeOfDay)
                .GroupBy(e => new {e.CreatedAt.Date, e.CreatedAt.Hour});

            foreach (var group in groups)
            {
                IChatRoomEvent firstEvent = group.FirstOrDefault();

                if (firstEvent == null) continue;

                report.AppendLine(firstEvent.CreatedAt.ToString("hh tt"));
                report.AppendLine();
                foreach (IChatRoomEvent chatEvent in group)
                {
                    report.AppendLine("     " + chatEvent);
                }
                report.AppendLine();
            }

            return report.ToString();
        }
    }
}