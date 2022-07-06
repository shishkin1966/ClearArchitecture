
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    public abstract class AbsSmallUnion : AbsProvider, ISmallUnion 
    {
        private readonly ISecretary<IProviderSubscriber> _secretary = CreateSecretary();

        private readonly ObserverObservable _observable = new ObserverObservable();

        public static ISecretary<IProviderSubscriber> CreateSecretary()
        {
            return new Secretary<IProviderSubscriber>();
        }

        protected AbsSmallUnion(string name) : base(name)
        {
        }

        public virtual bool ContainsSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return false;
            }

            return _secretary.ContainsKey(subscriber.GetName());
        }

        public virtual List<IProviderSubscriber> GetReadySubscribers()
        {
            List<IProviderSubscriber> subscribers = new List<IProviderSubscriber>();
            foreach (IProviderSubscriber subscriber in GetSubscribers())
            {
                if (subscriber.IsValid() && subscriber is ILifecycle)
                {
                    int state = (subscriber as ILifecycle).GetState();
                    if (state == Lifecycle.ON_READY)
                    {
                        subscribers.Add(subscriber);
                    }
                }
            }
            return subscribers;
        }
        
        public virtual IProviderSubscriber GetSubscriber(string name)
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

        public virtual List<IProviderSubscriber> GetSubscribers()
        {
            return _secretary.Values();
        }

        public virtual IProviderSubscriber GetValidSubscriber()
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

        public virtual List<IProviderSubscriber> GetValidSubscribers()
        {
            List<IProviderSubscriber> subscribers = new List<IProviderSubscriber>();
            subscribers.AddRange(from IProviderSubscriber subscriber in GetSubscribers()
                                 where subscriber.IsValid()
                                 select subscriber);
            return subscribers;
        }

        public virtual bool HasSubscriber(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return _secretary.ContainsKey(name);
        }

        public virtual bool HasSubscribers()
        {
            return !_secretary.IsEmpty();

        }

        public virtual void OnAddSubscriber(IProviderSubscriber subscriber)
        {
            // Method intentionally left empty.
        }

        public virtual void OnRegisterFirstSubscriber()
        {
            // Method intentionally left empty.
        }
        public virtual void OnUnRegisterLastSubscriber()
        {
            // Method intentionally left empty.
        }
        public virtual void OnUnRegisterSubscriber(IProviderSubscriber subscriber)
        {
            // Method intentionally left empty.
        }

        public virtual bool RegisterSubscriber(IProviderSubscriber subscriber) 
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

            if (_observable != null)
            {
                _observable.OnChangeObservable(subscriber);
            }

            if (cnt == 0 && _secretary.Size() == 1)
            {
                OnRegisterFirstSubscriber();
            }

            OnAddSubscriber(subscriber);

            return true;
        }

        public virtual void UnRegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return;
            }

            int cnt = _secretary.Size();
            // if (_secretary.ContainsKey(subscriber.GetName()) && (subscriber.GetType() == _secretary.GetValue(subscriber.GetName()).GetType()))
            if (_secretary.ContainsKey(subscriber.GetName()))
            {
                _secretary.Remove(subscriber.GetName());
                OnUnRegisterSubscriber(subscriber);
                subscriber.RemoveProvider(this.GetName());

                if (_observable != null)
                {
                    _observable.OnChangeObservable(subscriber);
                }
            }

            if (cnt == 1 && _secretary.Size() == 0)
            {
                OnUnRegisterLastSubscriber();
            }
        }

        public virtual void UnRegisterSubscriber(string name)
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

        public virtual IProviderSubscriber GetUnBusySubscriber()
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

        public virtual List<IProviderSubscriber> GetUnBusySubscribers()
        {
            List<IProviderSubscriber> list = new List<IProviderSubscriber>();
            list.AddRange(from IProviderSubscriber subscriber in GetSubscribers()
                          where !subscriber.IsBusy() && subscriber.IsValid()
                          select subscriber);
            return list;
        }

        public virtual void UnRegisterSubscribers()
        {
            foreach (IProviderSubscriber subscriber in GetSubscribers())
            {
                UnRegisterSubscriber(subscriber);
            }
            _secretary.Clear();
#if DEBUG
            Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Очистка списка зарегистрированных в "+GetName());
#endif
        }

        public override void Stop()
        {
            UnRegisterSubscribers();
            _observable.Stop();

            base.Stop();
        }


        public virtual void AddObserver(IObserver observer)
        {
            _observable.AddObserver(observer);
        }
    }
}
