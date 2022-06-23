using System.Collections.Concurrent;
using System.Threading;

namespace ClearArchitecture.SL
{
    public class LifecycleObservable : ILifecycleObservable
    {
        private ConcurrentBag<ILifecycle> _list = new ConcurrentBag<ILifecycle>();
        private int _state = Lifecycle.ON_CREATE;

        public LifecycleObservable(int state)
        {
            SetState(state);
        }

        public void AddLifecycleObserver(ILifecycle stateable)
        {
            foreach (ILifecycle s in _list)
            {
                if (s == stateable)
                {
                    return;
                }
            }

            stateable.SetState(_state);
            _list.Add(stateable);
        }

        public void ClearLifecycleObservers()
        {
            var bag = new ConcurrentBag<ILifecycle>();
            Interlocked.Exchange<ConcurrentBag<ILifecycle>>(ref _list, bag);
        }

        public int GetState()
        {
            return _state;
        }

        public void RemoveLifecycleObserver(ILifecycle stateable)
        {
            foreach (ILifecycle s in _list)
            {
                if (s == stateable)
                {
                    _list.TryTake(out stateable);
                    return;
                }
            }
        }

        public void SetState(int state)
        {
            _state = state;
            foreach (ILifecycle stateable in _list)
            {
                stateable.SetState(_state);
            }
            if (state == Lifecycle.ON_DESTROY)
            {
                ClearLifecycleObservers();
            }
        }
    }
}
