using System;

namespace ClearArchitecture.SL
{
    public abstract class AbsProvider : AbsSubscriber, IProvider
    {
        private readonly ObserverObservable _observable;

        protected AbsProvider(string name) : base(name)
        {
            _observable = new ObserverObservable(GetName() + "Observable");
        }

        public abstract int CompareTo(IProvider other);

        public virtual bool IsPersistent()
        {
            return false;
        }

        public virtual void OnRegister()
        {
#if DEBUG
                Console.WriteLine(DateTime.Now.ToString("G") + ": OnRegister provider " + GetName());
#endif
        }

        public virtual void OnUnRegister()
        {
#if DEBUG
                Console.WriteLine(DateTime.Now.ToString("G") + ": OnUnRegister provider " + GetName());
#endif
        }

        public virtual void Stop()
        {
            _observable.Stop();

#if DEBUG
                Console.WriteLine(DateTime.Now.ToString("G") + ": Stop provider " + GetName());
#endif
        }

        public virtual void AddObserver(IObserver observer)
        {
            _observable.AddObserver(observer);
        }

        public virtual IObserverObservable GetObservable()
        {
            return _observable;
        }
    }
}