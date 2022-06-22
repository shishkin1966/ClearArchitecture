using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public class ResultMessage : AbsMessage
    {
        private readonly ExtResult _result;

        private ResultMessage(string address) : base(address)
        {
        }

        private ResultMessage(List<string> address) : base(address)
        {
        }

        public ResultMessage(string address, ExtResult result) : this(address)
        {
            _result = result;
        }

        public ResultMessage(List<string> address, ExtResult result) : this(address)
        {
            _result = result;
        }

        private ResultMessage(ResultMessage message) : base(message)
        {
        }

        public ResultMessage(ResultMessage message, ExtResult result) : this(message)
        {
            _result = result;
        }

        public override IMessage Copy()
        {
            return new ResultMessage(this, _result);
        }

        public override void Read(IMessengerSubscriber subscriber)
        {
            if (subscriber == null) return;

            if (subscriber is IResponseListener listener)
            {
                listener.Response(_result);
            }
        }
    }
}
