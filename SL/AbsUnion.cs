using System;

namespace ClearArchitecture.SL
{
    public abstract class AbsUnion : AbsSmallUnion, IUnion
    {
        private WeakReference _currentSubscriber;

        protected AbsUnion(string name) : base(name)
        {

        }

        public new bool RegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return false;
            }

            if (base.RegisterSubscriber(subscriber)) {
                if (_currentSubscriber != null && _currentSubscriber.IsAlive)
                {
                    IProviderSubscriber oldSubscriber = (IProviderSubscriber)_currentSubscriber.Target;
                    if (subscriber.GetName() == (oldSubscriber.GetName()))
                    {
                        _currentSubscriber = new WeakReference(subscriber, false);
                    }
                }
                return true;
            }
            return false;
        }

        public new void UnRegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null) return;

            base.UnRegisterSubscriber(subscriber);

            if (_currentSubscriber != null && _currentSubscriber.IsAlive)
            {
                IProviderSubscriber oldSubscriber = (IProviderSubscriber)_currentSubscriber.Target;
                if (subscriber.GetName() == (oldSubscriber.GetName()))
                {
                    _currentSubscriber.Target = null;
                }
            }
        }

        public bool SetCurrentSubscriber(IProviderSubscriber subscriber) 
        {
            if (subscriber == null) return false;

            if (!subscriber.IsValid()) return false;

            if (!ContainsSubscriber(subscriber)) {
                RegisterSubscriber(subscriber);
            }
            
            if (!ContainsSubscriber(subscriber)) {
                return false;
            }

            _currentSubscriber = new WeakReference(subscriber, false);

            return true;
        }

        public IProviderSubscriber GetCurrentSubscriber() 
        {
            if (_currentSubscriber != null && _currentSubscriber.IsAlive)
            {
                return (IProviderSubscriber)_currentSubscriber.Target;
            }
            return default;
        }

    }
}
