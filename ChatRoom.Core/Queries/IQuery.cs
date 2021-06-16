using ChatRoom.Core.Queries.QueryArgs;

namespace ChatRoom.Core.Queries
{
    public interface IQuery<T> where T : IQueryArgs
    {
        object Run(T args);
    }
}