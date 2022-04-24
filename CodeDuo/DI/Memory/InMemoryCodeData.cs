using CodeDuo.Areas.DB.Data;
using CodeDuo.DS;
using System.Collections.Concurrent;

namespace CodeDuo.DI.Memory
{
    public class InMemoryCodeData
    {
        public string TempCode { get; set; }
        public DateTime LastUpdatedInMemoryDB { get; set; }
        public string OwnerId { get; set; }
        public ConcurrentDictionary<string, SharePermission> SharedUsers { get; set; } = new ConcurrentDictionary<string, SharePermission>();
    }
}
