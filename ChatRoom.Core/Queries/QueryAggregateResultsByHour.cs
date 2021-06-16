using System.Linq;
using System.Text;
using ChatRoom.Core.Events;
using ChatRoom.Core.Queries.QueryArgs;
using ChatRoom.Core.Repositories;

namespace ChatRoom.Core.Queries
{
    public class QueryAggregateResultsByHour : IQuery<AggregateByHourArgs>
    {
        private readonly IEventRepository<IChatRoomEvent> _eventRepository;

        public QueryAggregateResultsByHour(IEventRepository<IChatRoomEvent> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public object Run(AggregateByHourArgs args)
        {
            var report = new StringBuilder();
            var groups = _eventRepository.GetAll()
                .Where(i => i.ChatRoomId == args.ChatRoomId)
                .OrderByDescending(i => i.CreatedAt.Date)
                .ThenBy(i => i.CreatedAt.TimeOfDay)
                .GroupBy(e => new {e.CreatedAt.Date, e.CreatedAt.Hour});

            foreach (var group in groups)
            {
                var firstEvent = group.FirstOrDefault();

                if (firstEvent == null) continue;

                report.AppendLine(firstEvent.CreatedAt.ToString("hh tt"));
                report.AppendLine();

                var aggregates = group.GroupBy(i => i.GetType()).Select(g => new
                {
                    AggregateType = g.Key,
                    Total = g.Count()
                });

                foreach (var aggregate in aggregates)
                {
                    report.AppendLine(aggregate.Total + " " + aggregate.AggregateType.Name);
                }

                report.AppendLine();
            }

            return report.ToString();
        }
    }
}