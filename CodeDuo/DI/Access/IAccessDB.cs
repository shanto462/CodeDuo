using CodeDuo.DI.Memory;

namespace CodeDuo.DI.Access
{
    public interface IAccessDB
    {
        public bool IsInMemoryDB(Guid guid);
        void UpdateCodedata(Guid guid, string modified, int row, int column);
        InMemoryCodeData GetCodedata(Guid guid);
        InMemoryCodeData CreateCodedata(Guid guid);
        public bool IsInvalidGuid(string CodeId);
    }
}
