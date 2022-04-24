namespace CodeDuo.DI.Memory
{
    public interface IMemoryDB
    {
        void UpdateCodedata(Guid guid, string modified, int cursor);
        InMemoryCodeData GetCodedata(Guid guid);
        InMemoryCodeData CreateCodedata(Guid guid);
    }
}
