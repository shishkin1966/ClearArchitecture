using System;
using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    public class SubscriberObservable : AbsSubscriber, ISubscriberObservable
    {
        private readonly Secretary<IObservableSubscriber> _secretary = new Secretary<IObservableSubscriber>();

        public SubscriberObservable(string name) : base(name)
        {
        }

        public virtual void AddObserver(IObservableSubscriber subscriber)
        {
            if (subscriber == null) return;

            _secretary.Put(subscriber.GetName(), subscriber);
#if DEBUG
            Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Подключен observer " + subscriber.GetName() + " от observable " + GetName());
#endif
            OnRegisterObserver(subscriber);

            if (_secretary.Size() == 1)
            {
                OnRegisterFirstObserver();
            }
        }

        public virtual IObservableSubscriber GetObserver(string name)
        {
            if (string.IsNullOrEmpty(name)) return default;

            return _secretary.GetValue(name);
        }

        public virtual List<IObservableSubscriber> GetObservers()
        {
            return _secretary.Values();
        }

        public virtual void OnChangeObservable(object obj)
        {
            foreach (var subscriber in from IObservableSubscriber subscriber in _secretary.Values()
                                       where subscriber.IsValid()
                                       select subscriber)
            {
                subscriber.OnChangeObservable(GetName(), obj);
            }
        }

        public virtual void RemoveObserver(IObservableSubscriber subscriber)
        {
            if (subscriber == null) return;

            if (_secretary.ContainsKey(subscriber.GetName()))
            {
                if (subscriber == _secretary.GetValue(subscriber.GetName()))
                {
                    _secretary.Remove(subscriber.GetName());

                    OnUnRegisterObserver(subscriber);

#if DEBUG
                    Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Отключен observer " + subscriber.GetName()+ " от observable " + GetName());
#endif
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
#if DEBUG
            Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Stop observable " + GetName());
#endif
        }

        public virtual void OnRegisterObserver(IObservableSubscriber subscriber)
        {
            //
        }

        public virtual void OnUnRegisterObserver(IObservableSubscriber subscriber)
        {
            //
        }
    }
}
