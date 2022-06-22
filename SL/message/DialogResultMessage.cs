namespace ClearArchitecture.SL
{
    public class DialogResultMessage : AbsMessage
    {
        public const string SUBJ = "DialogResultMessage";

        private readonly DialogResultAction _action;

        public DialogResultMessage(string address, DialogResultAction action) : base(address)
        {
            _action = action;
        }

        public DialogResultMessage(DialogResultMessage message, DialogResultAction action) : base(message)
        {
            _action = action;
        }

        public new string GetSubj()
        {
            return SUBJ;
        }

        public override IMessage Copy()
        {
            return new DialogResultMessage(this, _action);
        }

        public override void Read(IMessengerSubscriber subscriber)
        {
            if (subscriber == null)
            {
                return;
            }

            if (subscriber is IDialogResultListener listener)
            {
                listener.OnDialogResult(_action);
            }
        }

    }
}
