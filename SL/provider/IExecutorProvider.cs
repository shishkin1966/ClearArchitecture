namespace ClearArchitecture.SL
{
    public interface IExecutorProvider : IProvider
    {
        void CancelRequests(string sender);

        void CancelRequest(string sender, string requestName);

        void CancelAll();

        void PutRequest(IRequest request);

        void RemoveRequest(IRequest request);
    }
}
