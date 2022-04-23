using CodeDuo.DI.Memory;

namespace CodeDuo.DI.Access
{
    public class AccessDB : IAccessDB
    {
        private readonly IMemoryDB _memoryDB;

        public AccessDB(IMemoryDB memoryDB)
        {
            _memoryDB = memoryDB;
        }

        public InMemoryCodeData CreateCodedata(Guid guid)
        {
            return _memoryDB.CreateCodedata(guid);
        }

        public InMemoryCodeData GetCodedata(Guid guid)
        {
            return _memoryDB.GetCodedata(guid);
        }

        public bool IsInMemoryDB(Guid guid)
        {
            return true;
        }

        public void UpdateCodedata(Guid guid, string modified, int row, int column)
        {
            _memoryDB.UpdateCodedata(guid, modified, row, column);
        }

        public bool IsInvalidGuid(string CodeId)
        {
            Guid guid;
            bool isValid = Guid.TryParse(CodeId, out guid);
            if (isValid)
            {
                var TempCodeData = _memoryDB.GetCodedata(guid);
                if (TempCodeData == null)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }
    }
}
