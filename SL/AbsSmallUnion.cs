
using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    public abstract class AbsSmallUnion : AbsProvider, ISmallUnion 
    {
        private readonly ISecretary<IProviderSubscriber> _secretary = CreateSecretary();

        public static ISecretary<IProviderSubscriber> CreateSecretary()
        {
            return new Secretary<IProviderSubscriber>();
        }

        protected AbsSmallUnion(string name) : base(name)
        {
        }

        public bool ContainsSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return false;
            }

            return _secretary.ContainsKey(subscriber.GetName());
        }

        public List<IProviderSubscriber> GetReadySubscribers()
        {
            List<IProviderSubscriber> subscribers = new List<IProviderSubscriber>();
            foreach (IProviderSubscriber subscriber in GetSubscribers())
            {
                if (subscriber.IsValid() && subscriber is ILifecycle)
                {
                    int state = (subscriber as ILifecycle).GetState();
                    if (state == Lifecycle.VIEW_READY)
                    {
                        subscribers.Add(subscriber);
                    }
                }
            }
            return subscribers;
        }
        
        public IProviderSubscriber GetSubscriber(string name)
        {
            if (!_secretary.ContainsKey(name))
            {
                return default;
            }
            else
            {
                return _secretary.GetValue(name);
            }
        }

        public List<IProviderSubscriber> GetSubscribers()
        {
            return _secretary.Values();
        }

        public IProviderSubscriber GetValidSubscriber()
        {
            foreach (IProviderSubscriber subscriber in GetSubscribers())
            {
                if (!subscriber.IsValid())
                {
                    return subscriber;
                }

            }
            return default;
        }

        public List<IProviderSubscriber> GetValidSubscribers()
        {
            List<IProviderSubscriber> subscribers = new List<IProviderSubscriber>();
            subscribers.AddRange(from IProviderSubscriber subscriber in GetSubscribers()
                                 where subscriber.IsValid()
                                 select subscriber);
            return subscribers;
        }

        public bool HasSubscriber(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return _secretary.ContainsKey(name);
        }

        public bool HasSubscribers()
        {
            return !_secretary.IsEmpty();

        }

        public void OnAddSubscriber(IProviderSubscriber subscriber)
        {
            // Method intentionally left empty.
        }

        public void OnRegisterFirstSubscriber()
        {
            // Method intentionally left empty.
        }
        public void OnUnRegisterLastSubscriber()
        {
            // Method intentionally left empty.
        }

        public bool RegisterSubscriber(IProviderSubscriber subscriber) 
        {
            if (subscriber == null)
            {
                return false;
            }

            if (!subscriber.IsValid())
            {
                return false;
            }

            int cnt = _secretary.Size();

            _secretary.Put(subscriber.GetName(), subscriber);
            subscriber.SetProvider(this.GetName());

            if (cnt == 0 && _secretary.Size() == 1)
            {
                OnRegisterFirstSubscriber();
            }

            OnAddSubscriber(subscriber);

            return true;
        }

        public void UnRegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return;
            }

            int cnt = _secretary.Size();
            if (_secretary.ContainsKey(subscriber.GetName()) && (subscriber.GetType() == _secretary.GetValue(subscriber.GetName()).GetType()))
            {
                _secretary.Remove(subscriber.GetName());
                subscriber.RemoveProvider(this.GetName());
            }

            if (cnt == 1 && _secretary.Size() == 0)
            {
                OnUnRegisterLastSubscriber();
            }
        }

        public void UnRegisterSubscriber(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            if (HasSubscriber(name))
            {
                IProviderSubscriber subscriber = GetSubscriber(name);
                UnRegisterSubscriber(subscriber);
            }
        }

         new public void Stop()
         {
            OnUnRegister();

            base.Stop();
         }

        new public void OnUnRegister()
        {
            UnRegisterSubscribers();

            base.OnUnRegister();
        }

        public IProviderSubscriber GetUnBusySubscriber()
        {
            foreach (IProviderSubscriber subscriber in GetSubscribers())
            {
                if (!subscriber.IsBusy() && subscriber.IsValid())
                {
                    return subscriber;
                }

            }
            return default;
        }

        public List<IProviderSubscriber> GetUnBusySubscribers()
        {
            List<IProviderSubscriber> list = new List<IProviderSubscriber>();
            list.AddRange(from IProviderSubscriber subscriber in GetSubscribers()
                          where !subscriber.IsBusy() && subscriber.IsValid()
                          select subscriber);
            return list;
        }

        public void UnRegisterSubscribers()
        {
            foreach (IProviderSubscriber subscriber in GetSubscribers())
            {
                UnRegisterSubscriber(subscriber);
                subscriber.OnStopProvider(GetName());
            }
            _secretary.Clear();
        }
    }
}
