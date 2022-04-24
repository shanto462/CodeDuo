using CodeDuo.DS;
using CodeDuo.Hubs;
using System.Collections.Concurrent;

namespace CodeDuo.DI.Memory.ConnectionCache
{
    public class ConnectionCache : IConnectionCache
    {
        private readonly ConcurrentMultiDictionary<string, ConnectedUser> _guidDict = new ConcurrentMultiDictionary<string, ConnectedUser>();

        private readonly ConcurrentMultiDictionary<string, ConnectedUser> _connectionDict = new ConcurrentMultiDictionary<string, ConnectedUser>();

        public IEnumerable<string> GetConnectionKeys(string guid)
        {
            if (_guidDict.ContainsKey(guid))
            {
                foreach (var pair in _guidDict[guid])
                {
                    yield return pair.ConnectionKey;
                }
            }
        }

        public bool RegisterConnection(string guid, string userId, string connectionKey)
        {
            if (!_guidDict.ContainsKey(guid))
            {
                var list = new ConcurrentHashset<ConnectedUser>();
                if (!AddToConnectedList(guid, userId, list, connectionKey))
                    return false;
                return _guidDict.TryAdd(guid, list);
            }
            else
            {
                return AddToConnectedList(guid, userId, _guidDict[guid], connectionKey);
            }
        }

        public bool RemoveFromCache(string connectionKey)
        {
            if (_connectionDict.ContainsKey(connectionKey))
            {
                var data = _connectionDict[connectionKey];
                foreach (var pair in data)
                {
                    var guid = pair.Guid;
                    var ret = _guidDict.TryRemove(guid);
                    if (!ret)
                        return false;
                }
                return _connectionDict.TryRemove(connectionKey);
            }
            return true;
        }

        private bool AddToConnectedList(string guid, string userId, ConcurrentHashset<ConnectedUser> list, string connectionKey)
        {
            var data = new ConnectedUser
            {
                Guid = guid,
                ConnectionKey = connectionKey,
                UserId = userId,
                ConnectionTime = DateTime.UtcNow
            };
            var ret = _connectionDict.TryAdd(connectionKey, data);
            if (!ret)
                return false;
            return list.TryAdd(data);
        }
    }
}
