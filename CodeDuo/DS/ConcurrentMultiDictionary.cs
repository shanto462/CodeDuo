using System.Collections;
using System.Collections.Concurrent;

namespace CodeDuo.DS
{
    public class ConcurrentMultiDictionary<K, V> : IEnumerable<K> where K : notnull where V : notnull
    {
        private readonly ConcurrentDictionary<K, ConcurrentHashset<V>> _map = new ConcurrentDictionary<K, ConcurrentHashset<V>>();

        public ConcurrentMultiDictionary() { }

        public ConcurrentHashset<V> this[K key]
        {
            get
            {
                if (ContainsKey(key))
                    return _map[key];
                throw new KeyNotFoundException("Key not found!");
            }
        }

        public bool TryAdd(K key, V val)
        {
            if (_map.ContainsKey(key))
            {
                return _map[key].TryAdd(val);
            }
            else
            {
                var list = new ConcurrentHashset<V>();
                var ret = list.TryAdd(val);
                if (!ret)
                    return false;
                return _map.TryAdd(key, list);
            }
        }

        public bool TryAdd(K key, ConcurrentHashset<V> valList)
        {
            if (_map.ContainsKey(key))
            {
                return false;
            }
            else
            {
                return _map.TryAdd(key, valList);
            }
        }

        public bool TryRemove(K key)
        {
            if (_map.ContainsKey(key))
                return _map.Remove(key, out _);
            return true;
        }

        public bool TryRemove(K key, V val)
        {
            if (_map.ContainsKey(key))
            {
                var list = _map[key];
                return list.TryRemove(val);
            }
            return true;
        }

        public bool ContainsKey(K key)
        {
            return _map.ContainsKey(key);
        }

        public IEnumerator<K> GetEnumerator()
        {
            return _map.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _map.Values.GetEnumerator();
        }
    }
}
