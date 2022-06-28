
namespace ClearArchitecture.SL
{
    public class SetFocusMessage : AbsMessage
    {
        public const string NAME = "SetFocusMessage";

        public SetFocusMessage(string address) : base(address)
        {
        }

        public override IMessage Copy()
        {
            return new SetFocusMessage(this.GetAddress());
        }

        public override string GetName()
        {
            return NAME;
        }
    }
}
