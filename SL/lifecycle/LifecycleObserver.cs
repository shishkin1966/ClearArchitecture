using System;

namespace ClearArchitecture.SL
{
    public class LifecycleObserver : ILifecycle
    {
        private int _state = Lifecycle.VIEW_CREATE;
        private readonly WeakReference _listener;

        public LifecycleObserver(ILifecycleListener listener)
        {
            if (listener != null)
            {
                _listener = new WeakReference(listener, false);
            }
            SetState(Lifecycle.VIEW_CREATE);

        }

        /**
        * Получить состояние объекта
        *
        * @return состояние объекта
        */
        public int GetState()
        {
            return _state;
        }

        /**
        * Установить состояние объекта
        *
        * @param state состояние объекта
        */
        public void SetState(int state)
        {
            _state = state;

            switch(state) 
            {
                case Lifecycle.VIEW_CREATE:
                    if (_listener.IsAlive)
                    {
                        ILifecycleListener l = _listener.Target as ILifecycleListener;
                        l.OnCreateView();
                    }
                    break;
                case Lifecycle.VIEW_READY:
                    if (_listener.IsAlive)
                    {
                        ILifecycleListener l = _listener.Target as ILifecycleListener;
                        l.OnReadyView();
                    }
                    break;
                case Lifecycle.VIEW_DESTROY:
                    if (_listener.IsAlive)
                    {
                        ILifecycleListener l = _listener.Target as ILifecycleListener;
                        l.OnDestroyView();
                    }
                    break;
            }
        }
    }
}
