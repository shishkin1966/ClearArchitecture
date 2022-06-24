using System;
using System.Collections.Generic;
using System.Linq;

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
            foreach (var subscriber in from IObservableSubscriber subscriber in _secretary.Values()
                                       where subscriber.IsValid()
                                       select subscriber)
            {
                subscriber.OnChangeObservable(GetName(), obj);
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

        public virtual void OnRegisterFirstObserver()
        {
            //
        }

        public virtual void OnUnRegisterLastObserver()
        {
            //
        }

        public virtual void Stop()
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
