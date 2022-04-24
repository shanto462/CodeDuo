using System.Collections.Concurrent;

namespace CodeDuo.DI.Memory
{

    public class MemoryDB : IMemoryDB
    {
        private readonly ConcurrentDictionary<Guid, InMemoryCodeData> _memoryDB = new ConcurrentDictionary<Guid, InMemoryCodeData>();

        public InMemoryCodeData GetCodedata(Guid guid)
        {
            return _memoryDB.ContainsKey(guid) ? _memoryDB[guid] : null;
        }

        public void UpdateCodedata(Guid guid, string modified, int cursor)
        {
            if (_memoryDB.ContainsKey(guid))
            {
                _memoryDB[guid].TempCode = modified;
            }
        }

        public InMemoryCodeData CreateCodedata(Guid guid, string userId)
        {
            InMemoryCodeData value = new()
            {
                TempCode = "",
                LastUpdatedInMemoryDB = DateTime.UtcNow,
                OwnerId = userId
            };
            _memoryDB.TryAdd(guid, value);
            return value;
        }

        public bool AddSharingToUserId(Guid guid, string userId)
        {
            if(_memoryDB.ContainsKey(guid))
            {
                var data = _memoryDB[guid];
                if(data.OwnerId == userId)
                    return true;
                if(data.SharedUsers.ContainsKey(userId))
                    return true;
                return data.SharedUsers.TryAdd(userId, Areas.DB.Data.SharePermission.WRITE);
            }
            return false;
        }
    }
}
