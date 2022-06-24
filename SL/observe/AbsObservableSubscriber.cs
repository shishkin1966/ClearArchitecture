using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsObservableSubscriber : AbsProviderSubscriber, IObservableSubscriber
    {
        protected AbsObservableSubscriber(string name) : base(name)
        {
        }

        public abstract List<string> GetObservable();

        public virtual int GetState()
        {
            return Lifecycle.ON_READY;
        }

        public abstract void OnChangeObservable(string observable, object obj);

        public virtual void OnStopObservable(string observable)
        {
            //
        }

        public virtual void SetState(int state)
        {
            //
        }
    }
}
