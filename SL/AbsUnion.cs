using System;

namespace ClearArchitecture.SL
{
    public abstract class AbsUnion : AbsSmallUnion, IUnion
    {
        private IProviderSubscriber _currentSubscriber;

        protected AbsUnion(string name) : base(name)
        {

        }

        public override bool RegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return false;
            }

            if (base.RegisterSubscriber(subscriber)) {
                if (_currentSubscriber != null)
                {
                    IProviderSubscriber oldSubscriber = _currentSubscriber;
                    if (subscriber.GetName() == (oldSubscriber.GetName()))
                    {
                        _currentSubscriber = subscriber;
                    }
                }
                return true;
            }
            return false;
        }

        public override void UnRegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null) return;

            base.UnRegisterSubscriber(subscriber);

            if (_currentSubscriber != null)
            {
                IProviderSubscriber oldSubscriber = _currentSubscriber;
                if (subscriber.GetName() == (oldSubscriber.GetName()))
                {
                    _currentSubscriber = null;
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

            _currentSubscriber = subscriber;

            return true;
        }

        public IProviderSubscriber GetCurrentSubscriber() 
        {
            return _currentSubscriber;
        }

    }
}
