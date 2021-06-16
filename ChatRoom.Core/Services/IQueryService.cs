using ChatRoom.Core.Queries.QueryArgs;

namespace ChatRoom.Core.Services
{
    public interface IQueryService<T> where T : IQueryArgs
    {
        string Execute(T args);
    }
}