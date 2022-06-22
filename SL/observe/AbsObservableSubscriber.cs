using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsObservableSubscriber : AbsProviderSubscriber, IObservableSubscriber
    {
        protected AbsObservableSubscriber(string name) : base(name)
        {
        }

        public abstract List<string> GetObservable();

        public int GetState()
        {
            return Lifecycle.VIEW_READY;
        }

        public abstract void OnChangeObservable(string observable, object obj);

        public void OnStopObservable(string observable)
        {
            //
        }

        public void SetState(int state)
        {
            //
        }
    }
}
