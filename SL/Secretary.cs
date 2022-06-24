using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    public class Secretary<T> : ISecretary<T>
    {
        private readonly ConcurrentDictionary<string, T> _subscribers = new ConcurrentDictionary<string, T>();

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            _subscribers.TryRemove(key, out T old);
        }

        public int Size()
        {
            return _subscribers.Count;
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

            _subscribers.AddOrUpdate(key, value, (_, oldValue) => oldValue);
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
            T value = default;

            if (string.IsNullOrEmpty(key))
            {
                return value;
            }

            if (ContainsKey(key))
            {
                if (_subscribers.TryGetValue(key, out value))
                {
                    return value;
                }
                else
                {
                    return default;
                }
            }
            return default;
        }

        public List<T> Values()
        {
            return _subscribers.Values.ToList();
        }

        public bool IsEmpty()
        {
            return (_subscribers.Count == 0);
        }

        public void Clear()
        {
            _subscribers.Clear();
        }

        public List<string> Keys()
        {
            return _subscribers.Keys.ToList();
        }
    }
}