using System.Collections.Generic;
using System.Linq;

namespace ClearArchitecture.SL
{
    public class ObservableUnion : AbsSmallUnion, IObservableUnion
    {
        public const string NAME = "ObservableUnion";

        private readonly Secretary<IObservable> _secretary = new Secretary<IObservable>();

        public ObservableUnion(string name) : base(name)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IObservableUnion)
            { return 0; }
            else
            { return 1; }
        }

        public IObservable GetObservable(string name)
        {
            return _secretary.GetValue(name);
        }

        public List<IObservable> GetObservables()
        {
            return _secretary.Values();
        }

        public void OnChangeObservable(string name, object obj)
        {
            if (string.IsNullOrEmpty(name)) return;

            IObservable observable = GetObservable(name);
            if (observable != null)
            {
                observable.OnChangeObservable(obj);
            }
        }

        public bool RegisterObservable(IObservable observable)
        {
            if (observable == null) return true;

            _secretary.Put(observable.GetName(), observable);
            return true;
        }

        public bool UnRegisterObservable(IObservable observable)
        {
            if (observable == null) return true;

            if (_secretary.ContainsKey(observable.GetName()))
            {
                if ( observable == _secretary.GetValue(observable.GetName()))
                {
                    _secretary.Remove(observable.GetName());
                    return true;
                }
            }
            return false;
        }

        new public void UnRegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null) return;

            base.UnRegisterSubscriber(subscriber);

            if (!(subscriber is IObservableSubscriber)) return;

            var s = subscriber as IObservableSubscriber;

            List<string> list = s.GetObservable();
            foreach (var observable in from IObservable observable in GetObservables()
                                       where list.Contains(observable.GetName())
                                       select observable)
            {
                observable.RemoveObserver(s);
            }
        }

        new public bool RegisterSubscriber(IProviderSubscriber subscriber)
        {
            if (subscriber == null) return true;

            if (!(subscriber is IObservableSubscriber)) return true;

            var s = subscriber as IObservableSubscriber;

            if (!base.RegisterSubscriber(subscriber)) return false;

            List<string> list = s.GetObservable();
            foreach (IObservable observable in GetObservables()) 
            {
                string name = observable.GetName();
                if (list.Contains(name)) {
                    observable.AddObserver(s);
                }
            }
            return true;
        }

        new public void OnUnRegister()
        {
            foreach (IObservable observable in GetObservables())
            {
                observable.Stop();
            }
            base.OnUnRegister();
        }

        new public void Stop()
        {
            OnUnRegister();
            base.Stop();
        }
    }
}
