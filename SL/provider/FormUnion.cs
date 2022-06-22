namespace ClearArchitecture.SL
{
    public class FormUnion : AbsSmallUnion
    {
        public const string NAME = "FormUnion";

        public FormUnion(string name) : base(name)
        {
        }

        public override int CompareTo(IProvider other)
        {
            if (other is IFormUnion)
            { return 0; }
            else
            { return 1; }
        }
    }
}
