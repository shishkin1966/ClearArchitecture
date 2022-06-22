namespace ClearArchitecture.SL
{
    public class PresenterUnion : AbsSmallUnion, IPresenterUnion
    {
        public const string NAME = "PresenterUnion";

        public PresenterUnion(string name) : base(name)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IPresenterUnion)
            { return 0; }
            else
            { return 1; }
        }

        public IPresenterSubscriber GetPresenter(string name)
        {
            return base.GetSubscriber(name) as IPresenterSubscriber;
        }
    }
}
