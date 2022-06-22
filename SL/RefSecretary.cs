using ClearArchitecture.SL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    class RefSecretary<T> : ISecretary<T>
    {
        private readonly ConcurrentDictionary<string, WeakReference> _subscribers = new ConcurrentDictionary<string, WeakReference>() ;

        public void Clear()
        {
            _subscribers.Clear();
        }

        public bool ContainsKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            return _subscribers.ContainsKey(key);
        }

        public T GetValue(string key)
        {
            T val = default(T);

            if (string.IsNullOrEmpty(key))
            {
                return val;
            }

            if (ContainsKey(key))
            {
                bool ret = _subscribers.TryGetValue(key, out WeakReference value);
                if (ret && value.IsAlive)
                {
                    val = (T)value.Target;
                }
            }
            return val;
        }

        public bool IsEmpty()
        {
            return _subscribers.Count == 0;
        }

        public List<string> Keys()
        {
            return _subscribers.Keys.ToList();
        }

        public void Put(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            if (value == null)
            {
                return;
            }

            _subscribers.AddOrUpdate(key, new WeakReference(value, false), (_, oldValue) => oldValue);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            _subscribers.TryRemove(key, out WeakReference old);
        }

        public int Size()
        {
            return _subscribers.Count;
        }

        public List<T> Values()
        {
            List<T> list = new List<T>();
            foreach (WeakReference r in _subscribers.Values)
            {
                if (r.IsAlive)
                {
                    list.Add((T)r.Target);
                }
            }
            return list;
        }
    }
}
