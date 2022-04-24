using CodeDuo.DI.Memory;

namespace CodeDuo.DI.Access
{
    public interface IAccessDB
    {
        public bool IsInMemoryDB(Guid guid);
        void UpdateCodedata(Guid guid, string modified, int cursor);
        InMemoryCodeData GetCodedata(Guid guid);
        InMemoryCodeData CreateCodedata(Guid guid, string userId);
        public bool IsInvalidGuid(string CodeId);
        bool AddSharingToUserId(Guid guid, string userId);
    }
}
