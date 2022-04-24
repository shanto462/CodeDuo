namespace CodeDuo.DI.Memory.ConnectionCache
{
    public interface IConnectionCache
    {
        IEnumerable<string> GetConnectionKeys(string guid);
        bool RegisterConnection(string guid, string userId, string connectionKey);
        bool RemoveFromCache(string connectionKey);
    }
}
