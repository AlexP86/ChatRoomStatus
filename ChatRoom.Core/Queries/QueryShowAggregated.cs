using System.Linq;
using System.Text;
using ChatRoom.Core.Events;
using ChatRoom.Core.Queries.QueryArgs;
using ChatRoom.Core.Repositories;

namespace ChatRoom.Core.Queries
{
    public class QueryShowAllAggregated : IQuery<ShowAllAggregatedArgs>
    {
        private readonly IEventRepository<IChatRoomEvent> _eventRepository;

        public QueryShowAllAggregated(IEventRepository<IChatRoomEvent> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public object Run(ShowAllAggregatedArgs args)
        {
            var report = new StringBuilder();
            var aggregates = _eventRepository.GetAll()
                .Where(i => i.ChatRoomId == args.ChatRoomId)
                .OrderByDescending(i => i.CreatedAt.Date)
                .ThenBy(i => i.CreatedAt.TimeOfDay)
                .GroupBy(i => i.GetType()).Select(g => new
                {
                    AggregateType = g.Key,
                    Total = g.Count()
                });

            foreach (var aggregate in aggregates)
            {
                report.AppendLine(aggregate.Total + " " + aggregate.AggregateType.Name);
            }

            return report.ToString();
        }
    }
}