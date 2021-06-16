using System.Collections.Generic;
using System.Linq;
using ChatRoom.Core.DataStore;
using ChatRoom.Core.Entities;
using ChatRoom.Core.Repositories;

namespace ChatRoom.Infrastructure.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity
    {
        private readonly IEntityDataStore _dataStore;

        public EntityRepository(IEntityDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public IList<T> GetAll()
        {
            return _dataStore.GetData().OfType<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dataStore.GetData().OfType<T>().Single(e => e.Id == id);
        }

        public void Create(T entity)
        {
            _dataStore.Add(entity);
        }

        public void Update(T entity)
        {
            var item = GetById(entity.Id);
            item = entity;
        }

        public void Delete(T entity)
        {
            _dataStore.Remove(entity.Id);
        }
    }
}