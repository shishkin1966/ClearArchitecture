namespace ClearArchitecture.SL
{
    public class ModelUnion : AbsSmallUnion, IModelUnion
    {
        public const string NAME = "ModelUnion";

        public ModelUnion(string name) : base(name)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other is ModelUnion)
            { return 0; }
            else
            { return 1; }
        }

        public IModelSubscriber GetModel(string name)
        {
            return base.GetSubscriber(name) as IModelSubscriber;
        }
    }
}
