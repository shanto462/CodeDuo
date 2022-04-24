using CodeDuo.Data;
using CodeDuo.DI.Access;
using CodeDuo.DI.Memory.ConnectionCache;
using CodeDuo.Extensions;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace CodeDuo.Hubs
{
    public class PlaygroundHub : Hub
    {
        private readonly IAccessDB _accessDB;
        private readonly IConnectionCache _connectionCache;

        public PlaygroundHub(IAccessDB accessDB, IConnectionCache connectionCache) : base()
        {
            _accessDB = accessDB;
            _connectionCache = connectionCache;
        }

        public async Task UpdateCode(string userId, string guid, string message, int cursor)
        {
            _accessDB.UpdateCodedata(Guid.Parse(guid), message, 0);
            foreach (var connectionKey in _connectionCache.GetConnectionKeys(guid))
            {
                await Clients.Clients(connectionKey).SendAsync("ReceiveBroadCast", guid, message);
            }
        }

        public async Task<bool> RegisterForCode(string guid, string userId)
        {
            return await Task.Run(() =>
            {
                if (_accessDB.IsInvalidGuid(guid))
                    return false;
                return _connectionCache.RegisterConnection(guid, userId, Context.ConnectionId);
            });
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
