namespace ClearArchitecture.SL
{
    public interface ILifecycleObservable : ILifecycle
    {
        void AddLifecycleObserver(ILifecycle stateable);

        void RemoveLifecycleObserver(ILifecycle stateable);

        void ClearLifecycleObservers();
    }
}
