using System.Collections.Generic;
using System.Linq;
using ChatRoom.Core.DataStore;
using ChatRoom.Core.Entities;

namespace ChatRoom.Infrastructure.DataStore
{
    public class EntitiesDataStore : IEntityDataStore
    {
        private static int _nextId = 1000;

        // this could be a dictionary, but it is data mock so never mind
        private readonly IList<object> _data = new List<object>
        {
            new ParticipantEntity {Id = 1, Name = "Peter"},
            new ParticipantEntity {Id = 2, Name = "Robert"},
            new ParticipantEntity {Id = 3, Name = "Mike"},
            new ParticipantEntity {Id = 4, Name = "Alice"},
            new ParticipantEntity {Id = 5, Name = "Lisa"},
            new ChatRoomEntity {Id = 20, Name = "Sports", PaticipantIds = new List<int>() },
            new ChatRoomEntity {Id = 21, Name = "Science", PaticipantIds = new List<int>() }
        };

        public IEnumerable<object> GetData()
        {
            return _data;
        }

        public void Add(object item)
        {
            var entity = item as IEntity;
            entity.Id = _nextId++;
            _data.Add(entity);
        }

        public void Remove(int id)
        {
            var item = _data.Single(i => (i as IEntity).Id == id);
            _data.Remove(item);
        }
    }
}