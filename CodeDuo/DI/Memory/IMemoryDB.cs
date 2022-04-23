namespace CodeDuo.DI.Memory
{
    public interface IMemoryDB
    {
        void UpdateCodedata(Guid guid, string modified, int row, int column);
        InMemoryCodeData GetCodedata(Guid guid);
        InMemoryCodeData CreateCodedata(Guid guid);
    }
}
