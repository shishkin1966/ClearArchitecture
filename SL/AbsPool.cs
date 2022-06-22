using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace ClearArchitecture.SL
{
    public abstract class AbsPool<T> : AbsProvider, IPool<T> where T : new()
    {
        private ConcurrentBag<T> _items = new ConcurrentBag<T>();
        private readonly int _capacity;

        public int Capacity
        {
            get
            {
                return _capacity;
            }
        }

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }


        protected AbsPool(string name, int capacity) : base(name)
        {
            _capacity = capacity;
        }

        public abstract T ObjectFactory();

        public List<T> Get(int count)
        {
            List<T> list = new List<T>();
            if (count < 1) return list;

            do
            {
                if (_items.TryTake(out T item))
                {
                    list.Add(item);
                }
                else
                {
                    if (_items.Count < _capacity)
                    { 
                        Release(ObjectFactory());
                    }
                    else
                    {
                        break;
                    }
                }

            } while (list.Count < count);
            return list;
        }

        public void Release(T item)
        {
            if (_items.Count < _capacity)
            {
                _items.Add(item);
            }
        }

        public void Release(List<T> items)
        {
            int i = 0;
            while  (_items.Count < _capacity) { 
                if (i < items.Count)
                {
                    _items.Add(items[i]);
                    i++;
                }
                else
                {
                    break;
                }
            }
        }

        new public void Stop()
        {
            var bag = new ConcurrentBag<T>();
            Interlocked.Exchange<ConcurrentBag<T>>(ref _items, bag);

            base.Stop();
        }

        public override string ToString()
        {
            return string.Format("Pool: {0} Capacity: {1} Count: {2}", GetName(), Capacity, Count);
        }
    }
}
