namespace ClearArchitecture.SL
{
    public class DialogResultAction : AbsAction
    {
        private readonly int _id;

        public DialogResultAction(int id)
        {
            _id = id;
        }

        public int GetId()
        {
            return _id;
        }
    }
}
