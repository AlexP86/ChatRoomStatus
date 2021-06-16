using ChatRoom.Core.Queries;
using ChatRoom.Core.Queries.QueryArgs;
using ChatRoom.Core.Services;

namespace ChatRoom.Infrastructure.Services
{
    public class QueryService<T> : IQueryService<T> where T : class, IQueryArgs
    {
        private readonly IQuery<T> _query;

        public QueryService(IQuery<T> query)
        {
            _query = query;
        }

        public string Execute(T args)
        {
            return _query.Run(args).ToString();
        }
    }
}