using System.Threading;

namespace ClearArchitecture.SL
{
    public class ExecutorProvider : AbsProvider, IExecutorProvider
    {
        public const string NAME = "ExecutorProvider";
        public const int ACTION_NOTHING = -1;
        public const int ACTION_DELETE = 0;
        public const int ACTION_IGNORE = 1;

        private readonly Secretary<IRequest> _requests = new Secretary<IRequest>();

        public ExecutorProvider(string name) : base(name)
        {
        }

        public override void OnRegister()
        {
            ThreadPool.SetMaxThreads(8, 4);

            base.OnRegister();
        }

        public void CancelAll()
        {
            foreach(IRequest request in _requests.Values())
            {
                request.SetCanceled();
            }
            _requests.Clear();
        }

        public void CancelRequests(string sender)
        {
            if (string.IsNullOrEmpty(sender)) return;

            foreach (IRequest request in _requests.Values())
            {
                if (request.GetSender() == sender)
                {
                    request.SetCanceled();
                    _requests.Remove(request.GetName());
                }
            }
        }

        public void CancelRequest(string sender, string requestName)
        {
            if (string.IsNullOrEmpty(sender)) return;

            if (string.IsNullOrEmpty(requestName)) return;

            foreach (IRequest request in _requests.Values())
            {
                if (request.GetSender() == sender && request.GetName() == requestName)
                {
                    request.SetCanceled();
                    _requests.Remove(request.GetName());
                }
            }
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IExecutorProvider)
            { return 0; }
            else
            { return 1; }
        }

        public void PutRequest(IRequest request)
        {
            if (request.IsSingle())
            {
                if (_requests.ContainsKey(request.GetName()))
                {
                    foreach (IRequest oldRequest in _requests.Values())
                    {
                        if (oldRequest.GetName() == request.GetName())
                        {
                            oldRequest.AddReceiver(request.GetReceiver());
                        }
                    }
                }
                else
                {

                    request.SetExecutor(this);
                    _requests.Put(request.GetName(), request);
                    ExecuteRequest(request);
                }
            }
            else
            {
                if (request.IsDistinct() && _requests.ContainsKey(request.GetName()))
                {
                    foreach (IRequest oldRequest in _requests.Values())
                    {
                        if (oldRequest.GetName() == request.GetName())
                        {
                            var action = request.GetAction(oldRequest);
                            if (action == ACTION_DELETE)
                            {
                                oldRequest.SetCanceled();
                            }
                        }
                    }
                }
                request.SetExecutor(this);
                _requests.Put(request.GetName(), request);
                ExecuteRequest(request);
            }
        }

        public void RemoveRequest(IRequest request)
        {
            if (request == null) return;

            _requests.Remove(request.GetName());
        }

        static private void ExecuteRequest(IRequest request)
        {
            if (request == null) return;

            ThreadPool.QueueUserWorkItem(request.Execute, request);
        }

        public override void Stop()
        {
            CancelAll();

            base.Stop();
        }
    }
}
