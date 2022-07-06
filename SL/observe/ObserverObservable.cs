using System.Collections.Generic;
using System.Linq;


namespace ClearArchitecture.SL
{
    public class ObserverObservable : IObserverObservable
    {
        private readonly Secretary<IObserver> _secretary = new Secretary<IObserver>();

        public ObserverObservable()
        {
        }

        public virtual IObserver GetObserver(string name)
        {
            if (string.IsNullOrEmpty(name)) return default;

            return _secretary.GetValue(name);
        }

        public virtual List<IObserver> GetObservers()
        {
            return _secretary.Values();
        }

        public virtual void OnChangeObservable(object obj)
        {
            foreach (var subscriber in from IObserver subscriber in _secretary.Values()
                                       select subscriber)
            {
                subscriber.OnChangeObservable(obj);
            }
        }

        public virtual void RemoveObserver(IObserver subscriber)
        {
            if (subscriber == null) return;

            if (_secretary.ContainsKey(subscriber.GetName()))
            {
                _secretary.Remove(subscriber.GetName());

#if DEBUG
                Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Отключен observer " + subscriber.GetName()+ " от observable " + GetName());
#endif
            }
        }

        public virtual void Stop()
        {
            foreach (var subscriber in from IObserver subscriber in _secretary.Values()
                                       select subscriber)
            {
                subscriber.OnStopObservable();
            }
            _secretary.Clear();
#if DEBUG
            Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Stop observable " + GetName());
#endif
        }

        public virtual void AddObserver(IObserver subscriber)
        {
            if (subscriber == null) return;

            _secretary.Put(subscriber.GetName(), subscriber);
#if DEBUG
            Console.WriteLine(DateTime.Now.ToString("G") + ": " + "Подключен observer " + subscriber.GetName() + " от observable " + GetName());
#endif
        }

    }
}
