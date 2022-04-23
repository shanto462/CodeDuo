using CodeDuo.Hubs;
using System.Collections.Concurrent;

namespace CodeDuo.DI.Memory.ConnectionCache
{
    public class ConnectionCache : IConnectionCache
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<ConnectedUser, byte>> _connectionDict = new ConcurrentDictionary<string, ConcurrentDictionary<ConnectedUser, byte>>();

        public IEnumerable<string> GetConnectionKeys(string guid)
        {
            if (_connectionDict.ContainsKey(guid))
            {
                foreach (var pair in _connectionDict[guid])
                {
                    yield return pair.Key.ConnectionKey;
                }
            }
        }

        public bool RegisterConnection(string guid, string userId, string connectionKey)
        {
            if (!_connectionDict.ContainsKey(guid))
            {
                var list = new ConcurrentDictionary<ConnectedUser, byte>();
                if (!AddToConnectedList(guid, userId, list, connectionKey))
                    return false;
                return _connectionDict.TryAdd(guid, list);
            }
            else
            {
                return AddToConnectedList(guid, userId, _connectionDict[guid], connectionKey);
            }
        }

        private bool AddToConnectedList(string guid, string userId, ConcurrentDictionary<ConnectedUser, byte> list, string connectionKey)
        {
            return list.TryAdd(new ConnectedUser
            {
                Guid = guid,
                ConnectionKey = connectionKey,
                UserId = userId,
                ConnectionTime = DateTime.UtcNow
            }, new byte());
        }
    }
}
