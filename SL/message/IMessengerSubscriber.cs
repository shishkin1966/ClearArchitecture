namespace ClearArchitecture.SL
{
    public interface IMessengerSubscriber : IProviderSubscriber, ILifecycle
    {
        void Read(IMessage message);
    }
}
