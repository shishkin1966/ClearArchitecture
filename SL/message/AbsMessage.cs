using System.Collections.Generic;

namespace ClearArchitecture.SL
{
    public abstract class AbsMessage : AbsAction, IMessage
    {
        private int _id = -1;
        private string _address;
        private readonly List<string> _copyTo = new List<string>();
        private long _keepAliveTime = -1L;
        private string _subj;

        protected AbsMessage() 
        {
        }

        protected AbsMessage (string address) : this()
        {
            _address = address;
        }

        protected AbsMessage(List<string> address) : this()
        {
            if (address != null && address.Count > 0)
            {
                _address = address[0];
                var copy = new List<string>();
                for (int i = 1; i < address.Count; i++)
                {
                    copy.Add(address[i]);
                }
                _copyTo.AddRange(copy);
            }
        }

        protected AbsMessage(IMessage message) : this(message.GetAddress())
        {
            _copyTo.AddRange(message.GetCopyTo());
            _id = message.GetMessageId();
            _keepAliveTime = message.GetEndTime();
            _subj = message.GetSubj();
        }

        public abstract IMessage Copy();
        public abstract void Read(IMessengerSubscriber subscriber);

        public bool ContainsAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return false;
            }

            if (address == _address)
            {
                return true;
            }

            foreach (string s in _copyTo)
            {
                if (s == address)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetAddress()
        {
            return _address;
        }

        public List<string> GetCopyTo()
        {
            return _copyTo;
        }

        public long GetEndTime()
        {
            return _keepAliveTime;
        }

        public int GetMessageId()
        {
            return _id;
        }

        public string GetSubj()
        {
            return _subj;
        }

        public bool IsCheckDublicate()
        {
            return false;
        }

        public bool IsSticky()
        {
            return false;
        }

        public IMessage SetAddress(string address)
        {
            _address = address;
            return this;
        }

        public IMessage SetCopyTo(List<string> copyTo)
        {
            _copyTo.Clear();
            _copyTo.AddRange(copyTo);
            return this;
        }

        public IMessage SetEndTime(long keepAliveTime)
        {
            _keepAliveTime = keepAliveTime;
            return this;
        }

        public IMessage SetMessageId(int id)
        {
            _id = id;
            return this;
        }

        public IMessage SetSubj(string subj)
        {
            _subj = subj;
            return this;
        }
    }
}
