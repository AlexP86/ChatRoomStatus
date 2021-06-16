using System.Collections.Generic;
using ChatRoom.Core.Entities;

namespace ChatRoom.Core.Repositories
{
    public interface IEntityRepository<T> where T : IEntity
    {
        IList<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}