using System.Collections.Concurrent;

namespace CodeDuo.DI.Memory
{
    public class InMemoryCodeData
    {
        public string TempCode { get; set; }
        public DateTime LastUpdatedInMemoryDB { get; set; }
    }
}
