using System;

namespace ClearArchitecture.SL
{
    public class LifecycleObserver : ILifecycle
    {
        private int _state = Lifecycle.ON_CREATE;
        private readonly WeakReference _listener;

        public LifecycleObserver(ILifecycleListener listener)
        {
            if (listener != null)
            {
                _listener = new WeakReference(listener, false);
            }
            SetState(Lifecycle.ON_CREATE);

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
                case Lifecycle.ON_CREATE:
                    if (_listener.IsAlive)
                    {
                        ILifecycleListener l = _listener.Target as ILifecycleListener;
                        l.OnCreate();
                    }
                    break;
                case Lifecycle.ON_START:
                    if (_listener.IsAlive)
                    {
                        ILifecycleListener l = _listener.Target as ILifecycleListener;
                        l.OnStart();
                    }
                    break;
                case Lifecycle.ON_READY:
                    if (_listener.IsAlive)
                    {
                        ILifecycleListener l = _listener.Target as ILifecycleListener;
                        l.OnReady();
                    }
                    break;
                case Lifecycle.ON_DESTROY:
                    if (_listener.IsAlive)
                    {
                        ILifecycleListener l = _listener.Target as ILifecycleListener;
                        l.OnDestroy();
                    }
                    break;
            }
        }
    }
}
