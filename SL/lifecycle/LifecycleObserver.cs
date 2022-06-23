using System;

namespace ClearArchitecture.SL
{
    public class LifecycleObserver : ILifecycle
    {
        private int _state = Lifecycle.ON_CREATE;
        private readonly ILifecycleListener _listener;

        public LifecycleObserver(ILifecycleListener listener)
        {
            if (listener != null)
            {
                _listener = listener;
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
                    _listener.OnCreate();
                    break;
                case Lifecycle.ON_START:
                    _listener.OnStart();
                    break;
                case Lifecycle.ON_READY:
                    _listener.OnReady();
                    break;
                case Lifecycle.ON_DESTROY:
                    _listener.OnDestroy();
                    break;
            }
        }
    }
}
