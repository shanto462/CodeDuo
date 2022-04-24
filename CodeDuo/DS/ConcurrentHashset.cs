using System.Collections;
using System.Collections.Concurrent;

namespace CodeDuo.DS
{
    public class ConcurrentHashset<T> : IEnumerable<T> where T : notnull
    {
        private readonly ConcurrentDictionary<T, byte> _dict = new ConcurrentDictionary<T, byte>();

        public ConcurrentHashset() { }

        public bool TryAdd(T val)
        {
            if (!ContainsKey(val))
                return _dict.TryAdd(val, new byte());
            return true;
        }

        public bool TryRemove(T val)
        {
            if (ContainsKey(val))
                return _dict.Remove(val, out _);
            return false;
        }

        public bool ContainsKey(T val)
        {
            return _dict.ContainsKey(val);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _dict.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict.Values.GetEnumerator();
        }
    }
}
