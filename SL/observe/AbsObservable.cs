using System;
using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsObservable : AbsSubscriber, IObservable
    {
        private readonly Secretary<IObservableSubscriber> _secretary = new Secretary<IObservableSubscriber>();

        protected AbsObservable(string name) : base(name)
        {
        }

        public void AddObserver(IObservableSubscriber subscriber)
        {
            _secretary.Put(subscriber.GetName(), subscriber);
    
            if (_secretary.Size() == 1)
            {
                OnRegisterFirstObserver();
            }
        }

        public IObservableSubscriber GetObserver(string name)
        {
            return _secretary.GetValue(name);
        }

        public List<IObservableSubscriber> GetObservers()
        {
            return _secretary.Values();

        }

        public void OnChangeObservable(object obj)
        {
            foreach (IObservableSubscriber subscriber in _secretary.Values())
            {
                if (subscriber.IsValid())
                {
                    subscriber.OnChangeObservable(GetName(), obj);
                }
            }
        }

        public void RemoveObserver(IObservableSubscriber subscriber)
        {
            if (_secretary.ContainsKey(subscriber.GetName()))
            {
                if (subscriber == _secretary.GetValue(subscriber.GetName()))
                {
                    _secretary.Remove(subscriber.GetName());
                }

                if (_secretary.IsEmpty())
                {
                    OnUnRegisterLastObserver();
                }
            }
        }

        public void OnRegisterFirstObserver()
        {
            //
        }

        public void OnUnRegisterLastObserver()
        {
            //
        }

        public void Stop()
        {
            foreach (IObservableSubscriber subscriber in _secretary.Values())
            {
                subscriber.OnStopObservable(GetName());
            }
            _secretary.Clear();
            Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Stop "+GetName());
        }

    }
}
