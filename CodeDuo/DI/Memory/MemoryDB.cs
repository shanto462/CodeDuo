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

        public InMemoryCodeData CreateCodedata(Guid guid)
        {
            InMemoryCodeData value = new()
            {
                TempCode = "",
                LastUpdatedInMemoryDB = DateTime.UtcNow,
            };
            _memoryDB.TryAdd(guid, value);
            return value;
        }
    }
}
