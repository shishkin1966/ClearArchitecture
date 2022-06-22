namespace ClearArchitecture.SL
{
    public interface IFormSubscriber : IProviderSubscriber, IActionListener, IActionHandler,
    ILifecycleObservable
    {
    }
}
