namespace ClearArchitecture.SL
{
    public class DataMessage : AbsMessage
    {
        private readonly object _data;


        public DataMessage(string address, object data) : base(address)
        {
            _data = data;
        }

        public override IMessage Copy()
        {
            return new DataMessage(GetAddress(), _data);
        }

        public override void Read(IMessengerSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return;
            }

            subscriber.Read(this);
        }
    }
}
